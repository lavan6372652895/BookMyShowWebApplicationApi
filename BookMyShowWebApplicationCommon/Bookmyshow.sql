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
    @languages INT,
	@movieTeaser nvarchar(max)
)
AS
BEGIN
    IF (@movieid = 0 OR @movieid IS NULL)
    BEGIN
        INSERT INTO Movie (moviename, Genre, posterpic, Moviecast, ReleaseDate, Duration, languages,Teaser)
        VALUES (@moviename, @Genre, @posterpic, @Moviecast, @ReleaseDate, @Duration, @languages,@movieTeaser);

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
            languages = @languages,
		Teaser=@movieTeaser
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


alter proc Sp_GetTicket(  
@bookingid int,
@userid int 
)  
as  
begin  
select Bk.BookingID,Bk.Totalamount,mv.moviename,mv.posterpic,lg.Languagename,sh.ShowDate,
sh.ShowTime,bk.numberofseats,th.Name,sn.Code,
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
inner join Languages as lg
on mv.languages=lg.ID
where Bk.BookingID=@bookingid and Bk.UserID=@userid
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
    INSERT INTO userActivity(token, userid, windowidentity)
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

alter proc sp_forgotpassword
@password varchar(250),
@email varchar(250)
as
begin
declare @statuscode int
begin try
update Users
set passwords=@password,
passwordmodifydate=getdate()
where UserName=@email
set @statuscode =200
select @statuscode as statuscode
end try
begin catch
set @statuscode =100
select @statuscode as statuscode
end catch
end

exec GetTheaterWithScreens

create table Notifications(
id int primary key identity(100,1),
title varchar(250),
Notificationmessage nvarchar(max),
fromnotification varchar(250),
tonotification varchar(250),
notificationdate varchar(250)
)
ALTER TABLE Notifications
ADD Readstatus VARCHAR(10) DEFAULT 'unread';
-- Add a check constraint to enforce valid values
ALTER TABLE Notifications
ADD CONSTRAINT chk_Readstatus CHECK (Readstatus IN ('unread', 'read', 'archived'));

alter proc Sp_AddShowtimeing(
@MovieID int,
@ShowDate date,
@showtime time,
@screenid int
)
As
begin
insert into Showtimes(MovieID,ShowDate,ShowTime,ScreenId)
values(@MovieID,@ShowDate,@showtime,@screenid)
--select * from Showtimes 
--where ShowtimeID=SCOPE_IDENTITY()
End

select * from Screen

select * from Movie

create proc Sp_getsingleuserdata(
@Email varchar(250)
)
as
begin
select *
from Users as us
where us.UserName=@Email
end

alter PROCEDURE Sp_paginationdata
    @pageIndex INT,
    @pageSize INT
	
AS
BEGIN
    -- Calculate total items count
    DECLARE @totalItems INT;
    SELECT @totalItems = COUNT(*) FROM Seats;

    -- Calculate the starting row number for the current page
    DECLARE @startRow INT = (@pageIndex - 1) * @pageSize + 1;
    DECLARE @endRow INT = @pageIndex * @pageSize;

    -- Retrieve paginated data
    ;WITH PaginatedSeats AS (
        SELECT 
            *,
            ROW_NUMBER() OVER (ORDER BY SeatID) AS RowNum
        FROM Seats
    )
    SELECT @totalItems AS TotalItems, * 
    FROM PaginatedSeats
    WHERE RowNum BETWEEN @startRow AND @endRow;
END

exec Sp_paginationdata 3,6


alter proc Sp_AddorUpdateRewview(
@id int,
@review varchar(250),
@rating decimal(10,3),
@movieName int,
@userid int
)
as
begin
if(@id=0 or @id=null)
begin
declare @Reviewid int
insert into Reviews(Review,Rating,movieName,Userid)
values(@review,@rating,@movieName,@userid)
set @Reviewid=SCOPE_IDENTITY()
select *
from Reviews
where Id=@Reviewid
end
else
begin
update Reviews
set Review=@review,Rating=@rating
where Id=@id and Userid=@userid
select *
from Reviews
where Id=@id
end
end

create proc Sp_GetAllReviews
as
begin
select * 
from Reviews
end

create proc Sp_GetReviewByMovieid
(
@movieid int
)
as
begin
select *
from Reviews
where movieName=@movieid
end


alter proc Sp_addorupdateEventorganizations(
@ID int,
@organizationname varchar(250),
@orglogo nvarchar(max),
@email varchar(250),
@Password nvarchar(max),
@orgAddress nvarchar(max),
@organizationdescription nvarchar(max)
)
as
begin
If(@ID=0 or @ID=null)
begin
insert into Eventorganizations(organizationname,orglogo,Email,Password,orgAddress,organizationdescription)
values(@organizationname,@orglogo,@email,@Password,@orgAddress,@organizationdescription)
set @ID =SCOPE_IDENTITY()
select * 
from Eventorganizations
where ID=@ID
end
else
begin
update Eventorganizations
set organizationname=@organizationname,
orglogo=@orglogo,
Email=@email,
Password=@Password,
orgAddress=@orgAddress,
organizationdescription=@organizationdescription
select * 
from Eventorganizations
where ID=@ID
end
end

alter proc Sp_addorupdateEvent(
@Eventid int,
@Eventtype varchar(250),
@Startdate date,
@Enddtae date,
@duration int,
@EventAddress nvarchar(max),
@Eventcontact nvarchar(max),
@Eventdesc nvarchar(max),
@Orgid int,
@BookingsStartdate datetime ,
@Eventposter nvarchar(max)
)
as
begin
If(@Eventid=0 or @Eventid=null)
begin
insert into Eventstable(Eventtype,Startdate,Enddtae,duration,EventAddress,Eventcontact,Eventdesc,Orgid,BookingsStartdate,Eventposter)
values(@Eventtype,@Startdate,@Enddtae,@duration,@EventAddress,@Eventcontact,@Eventdesc,@Orgid,@BookingsStartdate,@Eventposter)
set @Eventid =SCOPE_IDENTITY()
end
else
begin
update Eventstable 
set Eventtype=@Eventtype,
Startdate=@Startdate,
Enddtae=@Enddtae,
duration=@duration,
EventAddress=@EventAddress,
Eventcontact=@Eventcontact,
Eventdesc=@Eventdesc,
Orgid=@Orgid,
BookingsStartdate=@BookingsStartdate,
Eventposter=@Eventposter
where Eventid=@Eventid and Orgid =@Orgid
end
end


--create proc Sp_GetEventsbyoranaization(
--@Orgid int
--)
--as
--begin
--select *
--from Eventstable

Alter table Eventstable
add Eventposter Nvarchar(max)


create table EventTicketCategory(
CategoryId int primary key identity(500,1),
Categorytype varchar(250)
)
insert into EventTicketCategory(Categorytype)
values('General'),('VIP or luxury'),('Early bird tickets'),('Group package tickets'),('Giveaways')








