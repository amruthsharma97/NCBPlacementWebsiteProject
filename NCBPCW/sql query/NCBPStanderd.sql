create database [NCBP]

DROP TABLE [NCBP].[dbo].[CurriculumFeedback]

alter TABLE [NCBP].[dbo].[PreviousExamInfo] add 
[StateId] int not null,
[CountryId] int not null;

ALTER TABLE [NCBP].[dbo].[Lecturer] drop column [DrepartmentId];


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AppraisalFeedback'
CREATE TABLE [NCBP].[dbo].[AppraisalFeedback] (
    [Id] int IDENTITY(1,1) NOT NULL,
	[QuestionId] int not null,
    [LecturerId] int not null,
	[Feedback] int not null,
	[Year] int not null
);
GO

-- Creating table 'CurriculumFeedback'
CREATE TABLE [NCBP].[dbo].[CurriculumFeedback] (
    [Id] int IDENTITY(1,1) NOT NULL,
	[QuestionId] int not null,
    [SubjectId] int not null,
	[Feedback] varchar(5) not null,
	[Year] int not null
);
GO

-- Creating table 'AppraisalQuestionnaire'
CREATE TABLE [NCBP].[dbo].[AppraisalQuestionnaire] (
    [Id] int IDENTITY(1,1) NOT NULL,
	[Order] int not null,
    [Question] nvarchar(max)  NOT NULL,
	[Year] int not null
);
GO

-- Creating table 'CurriculumQuestionnaire'
CREATE TABLE [NCBP].[dbo].[CurriculumQuestionnaire] (
    [Id] int IDENTITY(1,1) NOT NULL,
	[Order] int not null,
    [Question] nvarchar(max)  NOT NULL,
	[Year] int not null
);
GO

-- Creating table 'Department'
CREATE TABLE [NCBP].[dbo].[Department] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Lecturer'
CREATE TABLE [NCBP].[dbo].[Lecturer] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
	[DrepartmentId] int not null,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Subject'
CREATE TABLE [NCBP].[dbo].[Subject] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
	[CourseId] int not null,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Country'
CREATE TABLE [NCBP].[dbo].[Country] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

CREATE TABLE [NCBP].[dbo].[State] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
	[CountryId] int not null,
    [IsActive] bit  NOT NULL
);
GO

CREATE TABLE [NCBP].[dbo].[City] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
	[StateId] int not null,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Permissions'
CREATE TABLE [NCBP].[dbo].[Permissions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Entitlements'
CREATE TABLE [NCBP].[dbo].[Entitlements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(50)  NOT NULL,
    [UpdatedDate] datetime  NULL,
    [UpdatedBy] nvarchar(50)  NULL,
    [PermissionId] int  NOT NULL,
    [StaffId] int  NOT NULL
);
GO

-- Creating table 'Staffs'
CREATE TABLE [NCBP].[dbo].[Staffs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(50)  NOT NULL,
    [RollNo] nvarchar(20)  NULL,
    [Qualification] nvarchar(50)  NULL,
    [Designation] nvarchar(30)  NULL,
    [Experiance] varchar(10)  NULL,
    [DateOfBirth] datetime  NULL,
    [DateOfJoin] datetime  NULL,
    [Department] nvarchar(20)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Phone] nvarchar(15)  NOT NULL,
    [Address] nvarchar(100)  NULL,
    [IsActive] bit  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(50)  NOT NULL,
    [UpdatedDate] datetime  NULL,
    [UpdatedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [NCBP].[dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [UserName] nvarchar(20)  NOT NULL,
    [Passcode] nvarchar(200)  NOT NULL,
    [IsFirstLogin] bit  NOT NULL,
    [IsAdmin] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(50)  NOT NULL,
    [UpdatedDate] datetime  NULL,
    [UpdatedBy] nvarchar(50)  NULL,
    [StaffId] int  NOT NULL
);
GO

-- Creating table 'Course'
CREATE TABLE [NCBP].[dbo].[Course] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Student'
CREATE TABLE [NCBP].[dbo].[StudentProfiles] (
	[Id]                 UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Aadnum]             NVARCHAR (12)    NOT NULL,
    [URNo]               NVARCHAR (10)    NULL,
    [FName]              NVARCHAR (20)    NOT NULL,
    [MName]              NVARCHAR (20)    NULL,
    [LName]              NVARCHAR (20)    NOT NULL,
	[DOB]                DATETIME         NOT NULL,
    [MNumber]            NVARCHAR (10)    NOT NULL,
    [EmailId]            NVARCHAR (50)   NOT NULL,
	[Passcode]			 nvarchar(200)  NOT NULL,
    [EnroledYear] int not null,
	[ExpectedYOP] int not null,
    [AddrLine1]          NVARCHAR (80)   NULL,
    [AddrLine2]          NVARCHAR (80)   NULL,
    [PostalCode]         NVARCHAR (6)     NULL,
	[CreatedDate] datetime  NOT NULL,
	[ApprovedDate] datetime  NULL,
    [ApprovedBy] nvarchar(50)  NULL,
    [Status]             INT              NOT NULL,  
	[CourseId] int not null
);
GO

-- Creating table 'PreviousExamInfo'
CREATE TABLE [NCBP].[dbo].[PreviousExamInfo] (
[Id]  int IDENTITY(1,1) NOT NULL,
[StudentId] uniqueidentifier not null,
[PreviousCourseName] varchar(50) not null,
[PreviousInstitution] varchar(50) not null,
[MonthAndYearOfPassing] datetime not null,
[Result] varchar(50) not null
);

-- Creating table 'Semester'
CREATE TABLE [NCBP].[dbo].[Semester] (
[Id]  int IDENTITY(1,1) NOT NULL,
[StudentId] uniqueidentifier not null,
[SemesterNo] int not null,
[Result] varchar(50) not null
);

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CurriculumFeedback'
ALTER TABLE [NCBP].[dbo].[CurriculumFeedback]
ADD CONSTRAINT [PK_CurriculumFeedback]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AppraisalFeedback'
ALTER TABLE [NCBP].[dbo].[AppraisalFeedback]
ADD CONSTRAINT [PK_AppraisalFeedback]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CurriculumQuestionnaire'
ALTER TABLE [NCBP].[dbo].[CurriculumQuestionnaire]
ADD CONSTRAINT [PK_CurriculumQuestionnaire]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AppraisalQuestionnaire'
ALTER TABLE [NCBP].[dbo].[AppraisalQuestionnaire]
ADD CONSTRAINT [PK_AppraisalQuestionnaire]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Lecturer'
ALTER TABLE [NCBP].[dbo].[Lecturer]
ADD CONSTRAINT [PK_Lecturer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Department'
ALTER TABLE [NCBP].[dbo].[Department]
ADD CONSTRAINT [PK_Department]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Subject'
ALTER TABLE [NCBP].[dbo].[Subject]
ADD CONSTRAINT [PK_Subject]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Country'
ALTER TABLE [NCBP].[dbo].[Country]
ADD CONSTRAINT [PK_Country]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Country'
ALTER TABLE [NCBP].[dbo].[State]
ADD CONSTRAINT [PK_State]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Country'
ALTER TABLE [NCBP].[dbo].[City]
ADD CONSTRAINT [PK_City]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Permissions'
ALTER TABLE [NCBP].[dbo].[Permissions]
ADD CONSTRAINT [PK_Permissions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Entitlements'
ALTER TABLE [NCBP].[dbo].[Entitlements]
ADD CONSTRAINT [PK_Entitlements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Staffs'
ALTER TABLE [NCBP].[dbo].[Staffs]
ADD CONSTRAINT [PK_Staffs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [NCBP].[dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StudentProfiles'
ALTER TABLE [NCBP].[dbo].[StudentProfiles] 
ADD CONSTRAINT [PK_StudentProfiles] 
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Course'
ALTER TABLE [NCBP].[dbo].[Course] 
ADD CONSTRAINT [PK_Course] 
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PreviousExamInfo'
ALTER TABLE [NCBP].[dbo].[PreviousExamInfo] 
ADD CONSTRAINT [PK_PreviousExamInfo] 
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO


-- Creating primary key on [Id] in table 'Semester'
ALTER TABLE [NCBP].[dbo].[Semester] 
ADD CONSTRAINT [PK_Semester] 
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO
-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StateId] in table 'PreviousExamInfo'
ALTER TABLE [NCBP].[dbo].[PreviousExamInfo]
ADD CONSTRAINT [FK_StatePreviousExamInfo]
    FOREIGN KEY ([StateId])
    REFERENCES [NCBP].[dbo].[State]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StatePreviousExamInfo'
CREATE INDEX [IX_FK_StatePreviousExamInfo]
ON [NCBP].[dbo].[PreviousExamInfo]
    ([StateId]);
GO

-- Creating foreign key on [CountryId] in table 'PreviousExamInfo'
ALTER TABLE [NCBP].[dbo].[PreviousExamInfo]
ADD CONSTRAINT [FK_CountryPreviousExamInfo]
    FOREIGN KEY ([CountryId])
    REFERENCES [NCBP].[dbo].[Country]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermissionEntitlement'
CREATE INDEX [IX_FK_CountryPreviousExamInfo]
ON [NCBP].[dbo].[PreviousExamInfo]
    ([CountryId]);
GO

-- Creating foreign key on [QuestionId] in table 'CurriculumFeedback'
ALTER TABLE [NCBP].[dbo].[CurriculumFeedback]
ADD CONSTRAINT [FK_CurriculumQuestionnairFeedback]
    FOREIGN KEY ([QuestionId])
    REFERENCES [NCBP].[dbo].[CurriculumQuestionnaire]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CurriculumQuestionnairFeedback'
CREATE INDEX [IX_FK_CurriculumQuestionnairFeedback]
ON [NCBP].[dbo].[CurriculumFeedback]
    ([QuestionId]);
GO

-- Creating foriegn key on [QuestionId] in table 'AppraisalFeedback'
ALTER TABLE [NCBP].[dbo].[AppraisalFeedback]
ADD CONSTRAINT [FK_AppraisalQuestionnairFeedback]
    FOREIGN KEY ([QuestionId])
    REFERENCES [NCBP].[dbo].[AppraisalQuestionnaire]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AppraisalQuestionnairFeedback'
CREATE INDEX [IX_FK_AppraisalQuestionnairFeedback]
ON [NCBP].[dbo].[AppraisalFeedback]
    ([QuestionId]);
GO

-- Creating foreign key on [SubjectId] in table 'CurriculumFeedback'
ALTER TABLE [NCBP].[dbo].[CurriculumFeedback]
ADD CONSTRAINT [FK_SubjectCurriculumFeedback]
    FOREIGN KEY ([SubjectId])
    REFERENCES [NCBP].[dbo].[Subject]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CurriculumQuestionnairFeedback'
CREATE INDEX [IX_FK_SubjectCurriculumFeedback]
ON [NCBP].[dbo].[CurriculumFeedback]
    ([SubjectId]);
GO

-- Creating foriegn key on [LecturerId] in table 'AppraisalFeedback'
ALTER TABLE [NCBP].[dbo].[AppraisalFeedback]
ADD CONSTRAINT [FK_LecturerAppraisalFeedback]
    FOREIGN KEY ([LecturerId])
    REFERENCES [NCBP].[dbo].[Lecturer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AppraisalQuestionnairFeedback'
CREATE INDEX [IX_FK_LecturerAppraisalFeedback]
ON [NCBP].[dbo].[AppraisalFeedback]
    ([LecturerId]);
GO

-- Creating foreign key on [DepartmentId] in table 'Lecturer'
ALTER TABLE [NCBP].[dbo].[Lecturer]
ADD CONSTRAINT [FK_DepartmentLecturer]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [NCBP].[dbo].[Department]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentLecturer'
CREATE INDEX [IX_FK_DepartmentLecturer]
ON [NCBP].[dbo].[Lecturer]
    ([DepartmentId]);
GO

-- Creating foreign key on [CourseId] in table 'Subject'
ALTER TABLE [NCBP].[dbo].[Subject]
ADD CONSTRAINT [FK_CourseSubject]
    FOREIGN KEY ([CourseId])
    REFERENCES [NCBP].[dbo].[Course]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryState'
CREATE INDEX [IX_FK_CourseSubject]
ON [NCBP].[dbo].[Subject]
    ([CourseId]);
GO

-- Creating foreign key on [CountryId] in table 'State'
ALTER TABLE [NCBP].[dbo].[State]
ADD CONSTRAINT [FK_CountryState]
    FOREIGN KEY ([CountryId])
    REFERENCES [NCBP].[dbo].[Country]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryState'
CREATE INDEX [IX_FK_CountryState]
ON [NCBP].[dbo].[State]
    ([CountryId]);
GO

-- Creating foreign key on [StateId] in table 'City'
ALTER TABLE [NCBP].[dbo].[City]
ADD CONSTRAINT [FK_StateCity]
    FOREIGN KEY ([StateId])
    REFERENCES [NCBP].[dbo].[State]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StateCity'
CREATE INDEX [IX_FK_StateCity]
ON [NCBP].[dbo].[City]
    ([StateId]);
GO

-- Creating foreign key on [PermissionId] in table 'Entitlements'
ALTER TABLE [NCBP].[dbo].[Entitlements]
ADD CONSTRAINT [FK_PermissionEntitlement]
    FOREIGN KEY ([PermissionId])
    REFERENCES [NCBP].[dbo].[Permissions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermissionEntitlement'
CREATE INDEX [IX_FK_PermissionEntitlement]
ON [NCBP].[dbo].[Entitlements]
    ([PermissionId]);
GO

-- Creating foreign key on [CourseId] in table 'StudentProfiles'
ALTER TABLE [NCBP].[dbo].[StudentProfiles]
ADD CONSTRAINT [FK_CourseStudentProfiles]
    FOREIGN KEY ([CourseId])
    REFERENCES [NCBP].[dbo].[Course]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseStudentProfiles'
CREATE INDEX [IX_FK_CourseStudentProfiles]
ON [NCBP].[dbo].[StudentProfiles]
    ([CourseId]);
GO

-- Creating foreign key on [CountryId] in table 'StudentProfiles'
ALTER TABLE [NCBP].[dbo].[StudentProfiles]
ADD CONSTRAINT [FK_CountryStudentProfiles]
    FOREIGN KEY ([CountryId])
    REFERENCES [NCBP].[dbo].[Country]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryStudentProfiles'
CREATE INDEX [IX_FK_CountryStudentProfiles]
ON [NCBP].[dbo].[StudentProfiles]
    ([CountryId]);
GO

-- Creating foreign key on [StateId] in table 'StudentProfiles'
ALTER TABLE [NCBP].[dbo].[StudentProfiles]
ADD CONSTRAINT [FK_StateStudentProfiles]
    FOREIGN KEY ([StateId])
    REFERENCES [NCBP].[dbo].[State]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermissionEntitlement'
CREATE INDEX [IX_FK_StateStudentProfiles]
ON [NCBP].[dbo].[StudentProfiles]
    ([StateId]);
GO

-- Creating foreign key on [CityId] in table 'StudentProfiles'
ALTER TABLE [NCBP].[dbo].[StudentProfiles]
ADD CONSTRAINT [FK_CityStudentProfiles]
    FOREIGN KEY ([CityId])
    REFERENCES [NCBP].[dbo].[City]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermissionEntitlement'
CREATE INDEX [IX_FK_CityStudentProfiles]
ON [NCBP].[dbo].[StudentProfiles]
    ([CityId]);
GO

-- Creating foreign key on [StudentId] in table 'StudentProfiles'
ALTER TABLE [NCBP].[dbo].[PreviousExamInfo]
ADD CONSTRAINT [FK_StudentProfilesPreviousExamInfo]
    FOREIGN KEY ([StudentId])
    REFERENCES [NCBP].[dbo].[StudentProfiles]
        ([Id])
    ON DELETE cascade ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentProfilesPreviousExamInfo'
CREATE INDEX [IX_FK_StudentProfilesPreviousExamInfo]
ON [NCBP].[dbo].[PreviousExamInfo]
    ([StudentId]);
GO

-- Creating foreign key on [StudentId] in table 'StudentProfiles'
ALTER TABLE [NCBP].[dbo].[Semester]
ADD CONSTRAINT [FK_StudentProfilesSemester]
    FOREIGN KEY ([StudentId])
    REFERENCES [NCBP].[dbo].[StudentProfiles]
        ([Id])
    ON DELETE cascade ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentProfilesSemester'
CREATE INDEX [IX_FK_StudentProfilesSemester]
ON [NCBP].[dbo].[Semester]
    ([StudentId]);
GO

-- Creating foreign key on [StaffId] in table 'Entitlements'
ALTER TABLE [NCBP].[dbo].[Entitlements]
ADD CONSTRAINT [FK_StaffEntitlement]
    FOREIGN KEY ([StaffId])
    REFERENCES [NCBP].[dbo].[Staffs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffEntitlement'
CREATE INDEX [IX_FK_StaffEntitlement]
ON [NCBP].[dbo].[Entitlements]
    ([StaffId]);
GO

-- Creating foreign key on [StaffId] in table 'Users'
ALTER TABLE [NCBP].[dbo].[Users]
ADD CONSTRAINT [FK_StaffUser]
    FOREIGN KEY ([StaffId])
    REFERENCES [NCBP].[dbo].[Staffs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffUser'
CREATE INDEX [IX_FK_StaffUser]
ON [NCBP].[dbo].[Users]
    ([StaffId]);
GO

-- --------------------------------------------------
-- Script has ended



drop database [NCBP]