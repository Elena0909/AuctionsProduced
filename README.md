# Auctions produced
The application allows users to bid on products offered by other users, classified into hierarchical categories. Products are characterized by start and end dates of the auction, starting prices, and transaction-specific currencies.

## Technologies Used:
* C# for application development.
* SQL Server for data storage.
* Domain Model and Data Mapper for managing data layers and business logic.
* Unit Testing with NUnit to validate over 200 scenarios, including validation constraints and workflow correctness.
* Mocking for isolated component testing.
* Logging for application monitoring and diagnostics.
* StyleCop for ensuring adherence to coding standards.

This application is structured in layers without a user interface, focusing on data integrity and operational correctness in a complex and dynamic auction environment.

## Functionalities:

### Auctions for products:

* Users can initiate auctions for products by specifying the start and end dates.
* Included validations: the start date cannot be in the past, and the end date must be after the start date.
* Auctions only accept positive starting prices and bids in a single currency specified at the beginning of the auction.
* The price offered in each auction must be strictly higher than the previous price but not more than 300% higher than the previous price.

### Completion of auctions:

* Auctions automatically conclude on the specified date or at the request of the product owner.
* A user cannot conclude an auction they did not initiate.

### Management of active auctions:

* A user cannot have more than a predefined number of active and unfinished auctions at the same time.
* The value of this number can be configured without requiring recompilation of the application.

### User scores and restrictions:

* Each user receives a seriousness score, calculated as the median of scores given by other users in the last 3 months.
* If a user's score drops below a predefined threshold, they may be prohibited from opening new auctions for a specified period.

### Additional validations and duplicate prevention:

* The system detects and prevents excessive offering of similar products based on semantic similarities in descriptions.
* Overlap is evaluated using Levenshtein distance adjusted for case sensitivity and punctuation.

