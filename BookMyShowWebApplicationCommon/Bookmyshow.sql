use LAVANBD
-- function for return seat IDs with SeatNumbe and levels information
ALTER FUNCTION Seatesperlevel(
    @bookingid INT
)
RETURNS NVARCHAR(250)
AS
BEGIN
    DECLARE @setle NVARCHAR(250)

    -- Temporary table to hold seat IDs
    DECLARE @seatnoo TABLE(
        SeatID NVARCHAR(250)
    )

    -- Insert seat IDs into the temporary table
    INSERT INTO @seatnoo (SeatID)
    SELECT value AS SeatID
    FROM Bookings 
    CROSS APPLY STRING_SPLIT(seatnumbers, ',') 
    WHERE BookingID = @bookingid

    -- Update seat IDs with SeatNumbe and levels information
    UPDATE snt
    SET snt.SeatID = CONCAT(ss.SeatNumber,'L', ss.levels)
    FROM @seatnoo snt
    INNER JOIN Seats ss ON snt.SeatID = ss.SeatID

    -- Aggregate the seat IDs
    SELECT @setle = STRING_AGG(SeatID, ',')
    FROM @seatnoo

    RETURN @setle
END


ALTER PROCEDURE [dbo].[AddNewMovies](
    @movieid INT,
    @moviename VARCHAR(250),
    @Genre INT,
    @posterpic NVARCHAR(MAX),
    @Moviecast NVARCHAR(MAX),
    @ReleaseDate DATE,
   -- @Rating DECIMAL(3,1),
    @Duration INT,
    @languages INT
)
AS
BEGIN
    IF (@movieid = 0 OR @movieid IS NULL)
    BEGIN
        INSERT INTO Movie (moviename, Genre, posterpic, Moviecast, ReleaseDate, Duration, languages)
        VALUES (@moviename, @Genre, @posterpic, @Moviecast, @ReleaseDate, @Duration, @languages);

        DECLARE @id INT;
        SET @id = SCOPE_IDENTITY();

        SELECT * FROM Movie WHERE movieid = @id;
    END
    ELSE
    BEGIN
        UPDATE Movie
        SET 
            moviename = @moviename,
            Genre = @Genre,
            posterpic = @posterpic,
            Moviecast = @Moviecast,
            ReleaseDate = @ReleaseDate,
            --Rating = @Rating,
            Duration = @Duration,
            languages = @languages
        WHERE movieid = @movieid;

        SELECT * FROM Movie WHERE movieid = @movieid;
    END
END;
ALTER PROCEDURE [dbo].[Sp_addBookings]
(
    @userid INT,
    @showid INT,
    @Bookingid INT,
    @noofseats INT,
    @seatno NVARCHAR(250),
	@totalamount decimal(10,3),
	@ticketAmount decimal(10,3),
	@GstAmount decimal(10,3),
	@platformcharges decimal(10,3)
	)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        IF (@Bookingid IS NULL OR @Bookingid = 0)
        BEGIN
            -- Insert into Bookings table
            INSERT INTO Bookings (UserID, ShowtimeID, BookingDateTime,[status],numberofseats,seatnumbers,Totalamount,
			TicketAmount,GstAmount,Platformcharges)
            VALUES (@userid, @showid, GETDATE(), 1, @noofseats, @seatno,@totalamount,@ticketAmount,@GstAmount,@platformcharges)

            DECLARE @id INT 
            SET @id = SCOPE_IDENTITY()

            declare @seatnoo TABLE (
                SeatID NVARCHAR(250)
            )

            -- Insert seat numbers into the temporary table
           insert into @seatnoo(SeatID)
		   select value as SeatID
		   from Bookings 
         CROSS APPLY STRING_SPLIT(seatnumbers, ',') where BookingID=@id

            -- Update Seats table
            UPDATE s
            SET s.IsBooked = 1
            FROM Seats s
            INNER JOIN @seatnoo as sn
            ON s.seatId = sn.SeatID

			select * from Bookings as bk
			where bk.BookingID=@id
        END

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION

        -- Handle the error
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE()

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END
ALTER PROCEDURE [dbo].[sp_AddNewActor]  
(
    @id INT,
    @Actorname VARCHAR(250),
    @profilepic NVARCHAR(MAX),
    @gender VARCHAR(250),
    @descriptions VARCHAR(250),
    @role INT
)  
AS  
BEGIN  
    BEGIN TRY  
        BEGIN TRANSACTION;  

        IF (@id = 0 OR @id IS NULL)  
        BEGIN  
            -- Insert new actor  
            INSERT INTO Actors (Actorname, profilepic, gender, descriptions, Roles)  
            VALUES (@Actorname, @profilepic, @gender, @descriptions, @role);  

            -- Get the inserted actor's ID  
            SET @id = SCOPE_IDENTITY();  
        END  
        ELSE  
        BEGIN  
            -- Update existing actor  
            UPDATE Actors  
            SET Actorname = @Actorname,  
                profilepic = @profilepic,  
                gender = @gender,  
                descriptions = @descriptions,  
                Roles = @role  
            WHERE id = @id;  
        END  

        -- Select the updated or inserted actor  
        SELECT * FROM Actors WHERE id = @id;  

        COMMIT TRANSACTION;  
    END TRY  
    BEGIN CATCH  
        IF @@TRANCOUNT > 0  
            ROLLBACK TRANSACTION;  

        -- Raise an error or handle the exception as needed  
        THROW;  
    END CATCH  
END

ALTER proc [dbo].[Sp_CityList]
as
begin
select * from City 
order by CityName
end

CREATE proc Sp_GetTicket(  
@bookingid int  
)  
as  
begin  
select Bk.BookingID,mv.moviename,mv.languages,sh.ShowDate,sh.ShowTime,bk.numberofseats,th.Name,sn.Code,  
dbo.Seatesperlevel(@bookingid) as SeatNumbers,Bk.BookingDateTime  
from Bookings as Bk  
inner join Showtimes as sh   
on sh.ShowtimeID=bk.ShowtimeID   
inner join Screen as sn  
on sn.ID=sh.ScreenId  
inner join Theaters as th   
on th.TheaterID=sn.TheatreID  
inner join Movie as mv  
on mv.movieid=sh.MovieID  
where Bk.BookingID=@bookingid  
end

CREATE proc Sp_ListOfActors  
as  
begin  
select a.id,a.Actorname,a.descriptions,a.gender,a.profilepic ,R.RoleName as RoleName  
from Actors as a  
inner join Roles as R on R.Id=a.Roles  
End

create proc Sp_listofGenre  
as  
begin  
select * from Genre  
end

Create proc Sp_Listofmovies  
as  
begin  
Select * from Movie  
End

CREATE proc Sp_ListofmovieSeates  
@showtimeid int  
as  
begin  
select s.* ,sh.ScreenId,sh.ShowTime,sh.ShowDate  
from Seats as s  
inner join Showtimes as sh on sh.ShowtimeID=s.ShowtimeID  
where s.ShowtimeID=@showtimeid  
end

create proc Sp_listofRoles  
as  
begin  
select * from Roles  
end


CREATE proc [dbo].[sp_Movie_Theaterlist]  
@movieid int,  
@cityid int  
as  
begin  
 SELECT DISTINCT t.*  
from Theaters as t   
 inner join  Screen as s on s.TheatreID=t.TheaterID   
 inner join Showtimes as sh on sh.screenId =s.ID  
 inner join Movie as m on m.movieid=sh.MovieID  
where t.CityName=@cityid and sh.MovieID=@movieid  
  
select * from Showtimes   
end

create proc sp_movieBookdates(  
@theaterid int  
)  
as  
begin  
select sh.*  
from Showtimes as sh  
inner join Screen as sc on sc.ID=sh.ScreenId   
inner join Theaters as ts on ts.TheaterID=sc.TheatreID   
where ts.TheaterID=@theaterid  
end


alter PROCEDURE Sp_UserLogin  
    @UserName NVARCHAR(250),  
    @Password NVARCHAR(250)  
AS  
BEGIN  
    DECLARE @AuthResult NVARCHAR(50);  
  
    BEGIN TRY  
        -- Check if the user exists and password matches (assuming passwords are stored hashed)  
        IF NOT EXISTS (SELECT * FROM Users WHERE UserName = @UserName AND Passwords = @Password)  
        BEGIN  
            -- User authentication failed  
            SET @AuthResult = 'Unauthenticated';  
        END  
        ELSE  
        BEGIN  
            -- User authenticated successfully  
    set  @AuthResult = (select us.Roles
		  from Users as us
		  where us.UserName=@UserName)
        END  
    END TRY  
    BEGIN CATCH  
        -- Error handling  
        SET @AuthResult = ERROR_MESSAGE();  
    END CATCH  
    -- Return the result  
    SELECT @AuthResult AS AuthResult;  
END
select * from Users

--create proc for Registration
alter proc Sp_Registration(
@fullname varchar(250),
@username varchar(250),
@phonenumber varchar(250),
@password varchar(250),
@Role varchar(250)
)
as
begin

insert into Users(FullName,UserName,phonenumber,passwords,IsActive,Roles)
values(@fullname,@username,@phonenumber,@password,1,@Role)
declare @id int
set @id=SCOPE_IDENTITY()
select us.* from Users as us
where us.userid=@id
end

select * from userActivety

CREATE PROCEDURE sp_userActivity
    @token VARCHAR(250),
    @Email VARCHAR(250),
    @windowidentity VARCHAR(250)
AS
BEGIN
    -- Variable to hold the UserID
    DECLARE @userid INT;

    -- Retrieve the UserID based on the provided email
    SELECT @userid = UserID
    FROM Users
    WHERE UserName = @Email;

    -- Check if UserID is found
    IF @userid IS NULL
    BEGIN
        -- Handle the case where the user is not found (e.g., raise an error or log)
        RAISERROR('User not found', 16, 1);
        RETURN;
    END

    -- Insert the activity record into the userActivity table
    INSERT INTO userActivity (token, userid, windowidentity)
    VALUES (@token, @userid, @windowidentity);

    -- Return the inserted record
    SELECT * 
    FROM userActivity
    WHERE id = SCOPE_IDENTITY();
END;
 --Add New Theaters
alter PROCEDURE Sp_AddNewTheaters
    @name NVARCHAR(200),
    @Location NVARCHAR(400),
    @Phone NVARCHAR(30),
    @stateid INT,
    @CityName INT,
    @Email VARCHAR(200)
AS
BEGIN
    DECLARE @id INT;
    DECLARE @role VARCHAR(250);

    -- Retrieve the user ID and role based on the provided email
    SELECT @id = ur.UserID, @role = ur.Roles
    FROM Users AS ur
    WHERE ur.UserName = @Email;

    BEGIN TRY
        -- Check if the user ID is not null and the role is 'Theaters'
        IF @id IS NOT NULL AND @role = 'Theaters'
        BEGIN
            INSERT INTO Theaters ([Name], CityName, [Location], Phone, stateid, ownersid)
            VALUES (@name, @CityName, @Location, @Phone, @stateid, @id);

			select *
			from Theaters
			where Theaters.TheaterID=SCOPE_IDENTITY()
        END
    END TRY
    BEGIN CATCH
        -- Handle any errors that may have occurred
        -- You can log the error or re-throw it
        THROW;
    END CATCH;
END;

alter proc Sp_AddNewScreen(
@Code varchar(250),
@Levels int,
@SeatsPerLevel int,
@TheatreID int,
@SeatPrice int
)
as 
begin
BEGIN TRY
insert into Screen(Code,Levels,SeatsPerLevel,TheatreID,SeatPrice)
values(@Code,@Levels,@SeatsPerLevel,@TheatreID,@SeatPrice)
      select *
	from Screen
     where Screen.ID=SCOPE_IDENTITY()
End try
Begin catch
return 'invalid data'
end catch
end





alter PROCEDURE GetTheaterWithScreens
AS
BEGIN
    SELECT 
        t.TheaterID,
        t.Name,
        t.Location,
        t.Phone,
        t.stateid,
        t.CityName,
        t.ownersid,
        
        (
            SELECT 
                s.id,
                s.code,
                s.levels,
                s.seatsPerLevel,
                s.theatreID,
                s.seatPrice
            FROM screen s
            WHERE s.theatreID = t.TheaterID
            FOR JSON PATH
        ) AS screen
    FROM Theaters t;
END;


exec GetTheaterWithScreens