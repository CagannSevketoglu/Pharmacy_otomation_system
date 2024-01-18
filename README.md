 # Prescription Management System for Saglik Bakanligi for pharmacies

this repository contains the source code for the prescription management system API.

# My Drive link

https://drive.google.com/drive/folders/10dovLhEbj_T4k-AqNmjaq1OW8MiKTktb?usp=sharing


# Assumptions and Design Choices

-The project assumes the use of a Microsoft SQL Server database

-C#



# Issues Encountered

-If I couldn't make the variable of the list where Excel data is stored public static, I would have been able to complete the data in a single form and use it in multiple forms if it had been made public static. However, because I couldn't define it as such, I had to pull it from two separate forms.

-Deleting a medication may result in an error if you attempt to delete it without clicking on the medication in the prescription section.

-When importing Excel into the program and converting it into a small database, there is a slowdown and delay in the program upon initial opening. This is due to the program's process of loading the data from Excel, which contains approximately 7700 records. Because importing all the data takes a long time, the program currently only imports the first 100 records.
