create or replace table Attendees
(
    AttendeeId  int auto_increment
        primary key,
    Name        longtext not null,
    Email       longtext not null,
    Phone       longtext not null,
    Institution longtext not null,
    IDNumber    longtext not null
)
    charset = utf8mb4;

create or replace table Authors
(
    AuthorId          int auto_increment
        primary key,
    Name              longtext not null,
    IDNumber          longtext not null,
    InstitutionalMail longtext not null,
    PersonalMail      longtext not null,
    PhoneNumber       longtext not null,
    Country           longtext not null,
    City              longtext not null,
    AcademicDegree    int      not null
)
    charset = utf8mb4;

create or replace table Congresses
(
    CongressId                  int auto_increment
        primary key,
    Name                        longtext                                         not null,
    StartDate                   datetime(6)                                      not null,
    EndDate                     datetime(6)                                      not null,
    Location                    longtext                                         not null,
    Guid                        longtext                                         null,
    MinHours                    int         default 0                            not null,
    Status                      tinyint(1)  default 0                            not null,
    fileCertificateConference   longtext                                         null,
    fileCertificateAttendance   longtext                                         null,
    fileCertificateExposure     longtext                                         null,
    fileFlayer                  longtext                                         null,
    EndDateNotificationApprove  datetime(6) default '0001-01-01 00:00:00.000000' not null,
    EndDateRegistrationAttendee datetime(6) default '0001-01-01 00:00:00.000000' not null,
    EndDateRegistrationExposure datetime(6) default '0001-01-01 00:00:00.000000' not null
)
    charset = utf8mb4;

create or replace table Rooms
(
    RoomId     int auto_increment
        primary key,
    Name       longtext not null,
    Capacity   int      not null,
    Location   longtext not null,
    CongressId int      not null,
    constraint FK_Rooms_Congresses_CongressId
        foreign key (CongressId) references Congresses (CongressId)
            on delete cascade
)
    charset = utf8mb4;

create or replace table Exposures
(
    ExposureId      int auto_increment
        primary key,
    Name            longtext                                         not null,
    StatusExposure  int                                              not null,
    ResearchLine    int                                              not null,
    RoomId          int                                              null,
    SummaryFilePath longtext                                         not null,
    CongressId      int         default 0                            not null,
    Guid            longtext                                         null,
    DateStart       datetime(6)                                      not null,
    Type            int         default 0                            not null,
    DateEnd         datetime(6) default '0001-01-01 00:00:00.000000' not null,
    Observation     longtext                                         not null,
    Presented       longtext                                         not null,
    GuidCert        longtext                                         null,
    UrlAccess       longtext                                         not null,
    constraint FK_Exposures_Congresses_CongressId
        foreign key (CongressId) references Congresses (CongressId)
            on delete cascade,
    constraint FK_Exposures_Rooms_RoomId
        foreign key (RoomId) references Rooms (RoomId)
)
    charset = utf8mb4;

create or replace table Attendances
(
    AttendanceId int auto_increment
        primary key,
    Date         datetime(6) not null,
    AttendeeId   int         not null,
    ExposureId   int         not null,
    constraint FK_Attendances_Attendees_AttendeeId
        foreign key (AttendeeId) references Attendees (AttendeeId)
            on delete cascade,
    constraint FK_Attendances_Exposures_ExposureId
        foreign key (ExposureId) references Exposures (ExposureId)
            on delete cascade
)
    charset = utf8mb4;

create or replace index IX_Attendances_AttendeeId
    on Attendances (AttendeeId);

create or replace index IX_Attendances_ExposureId
    on Attendances (ExposureId);

create or replace table ExposureAuthors
(
    ExposureAuthorId int auto_increment
        primary key,
    Position         int not null,
    ExposureId       int not null,
    AuthorId         int not null,
    constraint FK_ExposureAuthors_Authors_AuthorId
        foreign key (AuthorId) references Authors (AuthorId)
            on delete cascade,
    constraint FK_ExposureAuthors_Exposures_ExposureId
        foreign key (ExposureId) references Exposures (ExposureId)
            on delete cascade
)
    charset = utf8mb4;

create or replace index IX_ExposureAuthors_AuthorId
    on ExposureAuthors (AuthorId);

create or replace index IX_ExposureAuthors_ExposureId
    on ExposureAuthors (ExposureId);

create or replace index IX_Exposures_CongressId
    on Exposures (CongressId);

create or replace index IX_Exposures_RoomId
    on Exposures (RoomId);

create or replace index IX_Rooms_CongressId
    on Rooms (CongressId);

create or replace table Users
(
    UserId       int auto_increment
        primary key,
    Name         longtext not null,
    Email        longtext not null,
    PasswordHash longblob not null,
    PasswordSalt longblob not null,
    Role         int      not null
)
    charset = utf8mb4;

create or replace table __EFMigrationsHistory
(
    MigrationId    varchar(150) not null
        primary key,
    ProductVersion varchar(32)  not null
);

INSERT INTO V1.Users (UserId, Name, Email, PasswordHash, PasswordSalt, Role) VALUES (1, 'Admin', 'correo@correo.com', 0x94AFD645C94F916E84C73FF0A494697CBEF8BCC02A223E462499BB7D2FFF5FC3AC287870BDC7E166B513A5DC43A8DC62E2172CBE21BFEA059C05C1637901EEE8, 0xBA0B40E17416DB118443EEA3A5DC12C12E3C7D39C5A62FBE18C7691D0A75F12889C2BA8BEC126809B539624B836F13E458DE7731BC95173AA621344B6DCEF49B12DDAB869FB9D1642A3D26DD68C9307DA358ECBABA58794E4A269FC36294BFDF0C096C1A7CE36EB874FC269F8D484149976E7C5173D92C250DF2AB82DA35A9F8, 0);
