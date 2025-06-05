CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "CoverLetters" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_CoverLetters" PRIMARY KEY,
    "ProfileId" TEXT NOT NULL,
    "JobPostingId" TEXT NOT NULL,
    "Content" TEXT NOT NULL
);

CREATE TABLE "JobPostings" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_JobPostings" PRIMARY KEY,
    "Title" TEXT NOT NULL,
    "Company" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "Stage" TEXT NOT NULL
);

CREATE TABLE "Profiles" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Profiles" PRIMARY KEY,
    "FullName" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Phone" TEXT NOT NULL,
    "Summary" TEXT NOT NULL
);

CREATE TABLE "Resumes" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Resumes" PRIMARY KEY,
    "ProfileId" TEXT NOT NULL,
    "Content" TEXT NOT NULL
);

CREATE TABLE "ProfileEducation" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ProfileEducation" PRIMARY KEY,
    "ProfileId" TEXT NOT NULL,
    CONSTRAINT "FK_ProfileEducation_Profiles_ProfileId" FOREIGN KEY ("ProfileId") REFERENCES "Profiles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ProfileExperience" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ProfileExperience" PRIMARY KEY,
    "ProfileId" TEXT NOT NULL,
    CONSTRAINT "FK_ProfileExperience_Profiles_ProfileId" FOREIGN KEY ("ProfileId") REFERENCES "Profiles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ProfileLanguages" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ProfileLanguages" PRIMARY KEY,
    "ProfileId" TEXT NOT NULL,
    CONSTRAINT "FK_ProfileLanguages_Profiles_ProfileId" FOREIGN KEY ("ProfileId") REFERENCES "Profiles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ProfileSkills" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ProfileSkills" PRIMARY KEY,
    "ProfileId" TEXT NOT NULL,
    CONSTRAINT "FK_ProfileSkills_Profiles_ProfileId" FOREIGN KEY ("ProfileId") REFERENCES "Profiles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ResumeEducation" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ResumeEducation" PRIMARY KEY,
    "ResumeId" TEXT NOT NULL,
    CONSTRAINT "FK_ResumeEducation_Resumes_ResumeId" FOREIGN KEY ("ResumeId") REFERENCES "Resumes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ResumeExperience" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ResumeExperience" PRIMARY KEY,
    "ResumeId" TEXT NOT NULL,
    CONSTRAINT "FK_ResumeExperience_Resumes_ResumeId" FOREIGN KEY ("ResumeId") REFERENCES "Resumes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ResumeSkills" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ResumeSkills" PRIMARY KEY,
    "ResumeId" TEXT NOT NULL,
    CONSTRAINT "FK_ResumeSkills_Resumes_ResumeId" FOREIGN KEY ("ResumeId") REFERENCES "Resumes" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_ProfileEducation_ProfileId" ON "ProfileEducation" ("ProfileId");

CREATE INDEX "IX_ProfileExperience_ProfileId" ON "ProfileExperience" ("ProfileId");

CREATE INDEX "IX_ProfileLanguages_ProfileId" ON "ProfileLanguages" ("ProfileId");

CREATE INDEX "IX_ProfileSkills_ProfileId" ON "ProfileSkills" ("ProfileId");

CREATE INDEX "IX_ResumeEducation_ResumeId" ON "ResumeEducation" ("ResumeId");

CREATE INDEX "IX_ResumeExperience_ResumeId" ON "ResumeExperience" ("ResumeId");

CREATE INDEX "IX_ResumeSkills_ResumeId" ON "ResumeSkills" ("ResumeId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250605082344_Init', '8.0.0');

COMMIT;

