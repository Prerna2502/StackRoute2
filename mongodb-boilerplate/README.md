## MongoDB Assignment

### Objective
Write MongoDB queries to understand basic CRUD operations in mongoDB database.

### Problem Statement
Create MongoDB queries based on high level requirements

### High Level Requirements
- Create a collection Products & insert the rows into the created collection as per below and add the script in `queries.json` file in `InsertScript` key.

| ProductID | Name     | Brand    | Quantity | Price |
|-----------|----------|----------|:--------:|-------|
|P001       |Laptop    |Dell      |5         |32000  |
|P002       |Tablet    |Lenovo    |7         |15000  |
|P003       |Camera    |Nikon     |3         |22000  |
|P004       |Smartphone|Samsung   |8         |25000  |
|P005       |Speaker   |Bose      |4         |12000  |

- Query1 - Fetch the row from Products collection where ProductID=P003
- Query2 - Fetch the rows from Products collection where Price range between 10000 and 20000
- Query3 - Fetch the rows from Products collection where Quantity>5
- Query4 - Fetch the rows from Products collection where product name contains `top` as last three letters for its name.
- Query5 - Fetch the rows from Products collection where the price of the product is not greater than 20000
- Query6 - Write a MongoDB query to arrange the name of the products in descending order along with all the columns.
- Query7 - Write a query to calculate the total sum of all products in Products collection.
- Query8 - Write a query to calculate the multiplication of Quantity & Price as Total and display along with all the columns.
- Query9 - Write a query to change the Price to 20000 where ProductID=P002
- Query10 - Write a query to delete the row from Products collection with Brand=Samsung

### Duration: 1-2 hours

### Flow of Modules
- Write all queries in the given `queries.json` file in Sequence and key names.
- Run test cases

### Tech Stack
- MongoDB