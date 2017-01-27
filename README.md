# GroceryCo.Kisok

Welcome to my Grocery Kiosk! This readme will explain some of the design decisions I made while creating it. First I will go over the 
architecture of the application, then I will talk about why certain decisions were made and cover some limitations of the system along 
with examples on how to use it correctly.

##Application Architecture
The Grocery Kiosk is broken down into three different sections. The Console Application (or presentation layer), the ‘Core’ Grocery Kiosk 
(Or Business Layer) and finally the Data Access Layer (DAL).

The Console Application handles running the program as well as taking user input and formatting output for the user into a readable form. 

The Core Grocery Kiosk handles the business logic of mapping the user input into a Basket, applying promotions if applicable, and 
calculating the total, which it then passes back to the presentation layer for rendering.

The DAL handles retrieving more static data, that in this case being the Kiosk Catalog Items, and the Promotions that are stored in the
appropriate CSV files.

The reason I decided to separate the application into these components was so that it would allow easy extensibility or change to either 
the DAL or presentation layer without having to modify the ‘Core’ business logic of the grocery kiosk. This way the DAL can be swapped 
from CSV files to entity framework if necessary or the Console Application can be changed to a Web Application if the need should arise.

###Other Design Decisions
I decided to use CSV files to store the information about Kiosk Items and Kiosk Promotions. To read from the CSV files I decided to use 
CSVHelper. This is mostly because I feel that reinventing the wheel for reading from CSV files is unnecessary and CSVHelper easily 
extensible, meaning that all you need to provide is a proper mapping file for a CSV file and it will map it to a custom object that you 
define in code.

The way I decided to solve the Promotions in a generic enough way to include the **_Advanced Requirements_** was to break down what the 
promotions actually meant mathematically. It is easy enough to do when you have a single item promotion. For each item you reduce it by 
the sale amount and you are done. For the 'Group' promotion and 'Additional Product Discount' what it really comes down to is you are 
discounting a certain quantity of items, so if you create the promotion with the sale price and the quantity you can just compare that
to your basket items and if they meet the threshold quantity you can subtract the difference from the total.

For example, if you have regular priced apples for $1 and you have a promotion for buy one get one 50% off, what you are saying 
mathematically is for 2 apples you will pay $1.50, which is a discount of $0.50, which can be taken off the total of the basket.


##Limitations
The key limitation of the system stems from an assumption I made about how to handle applying promotions to kiosk items. 
That being that you can only apply 1 promotion per item. Multiple promotions for the same item will be ignored, only the first promotion 
in the list will be applied.

##Examples
An Example ItemCatalog.csv file would be the following:
```
Id,Name,Price
1,Apple,1
2,Banana,2
3,Orange,0.5
```
Id's should always be unique to each item. Not doing so may cause application issues.

An Example PromotionCatalog.csv file would be the following:
```
Id,ItemId,SalePrice,Description,Quantity
1,1,0.75,25 cents off regular priced Apples, 1
2,2,4,Buy 3 Banana's for $2.00,3
3,3,0.75,Buy one Orange get the second 50% off,2
```
If using the ItemCatalog from above these promotions satisfy the Promotion Specifications given in the Coding Problem PDF.

##Configuration
The location for the PromotionsCatalog and ItemsCatalog CSV files can be specified in the app.config file under `<appConfig>`. Full file 
paths are 
recommended.

###Logging
I have implemented a very basic logging strategy using Log4Net. The way it works is on an unhandled exception the application will log
the error and then close. The log file is configured to output in the root of the application. If this application were to be installed 
properly the configuration file would need to modified to output the log file into a location on the machine it had the proper 
privileges to write a log file to. To view details on how the logging is configured view the app.config file 
under `<log4net>`.

###Error Scenarios
A couple error scenarios to be aware of surround reading the CSV files into the application. If the columns are not properly named as 
in the above examples then the application will error out. The user creating the CSV files should also be aware the the Id's for both 
Promotions and Catalog Items are intended to be unique.

##Third Party Libraries
1.	Autofac: Used for Dependency Injection.
2.	CSVHelper: Used for reading from CSV files.

