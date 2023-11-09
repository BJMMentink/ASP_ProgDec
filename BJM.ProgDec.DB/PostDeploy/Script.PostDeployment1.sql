﻿/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\DefaultData\Declarations.sql
:r .\DefaultData\DegreeTypes.sql
:r .\DefaultData\Programs.sql
:r .\DefaultData\Students.sql
:r .\DefaultData\Advisors.sql
:r .\DefaultData\StudentAdvisors.sql
