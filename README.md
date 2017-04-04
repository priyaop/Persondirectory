# Sample Rest Web api for a Phone Directory in .NET
This sample Phone directory project does
1) Lists out all records from Database
2) Searches the records with query parameters
3) Inserts new records into database by using JSON input
For Example:

To display all records from database:
GetPerRec(): httpget
header name :Content-Type and header value : application/json
Limits out first 2 database records 

To search for records with given query paramerters:
Eg1: With single parameter:
http://localhost:59644/api/Search/PerSearch?rname=PRIYA
lists all recs from db with rname = priya
Eg2: With multi parameter:
http://localhost:59644/api/Search/PerSearch?rname=PRIYA&raddr=maple shade

To Insert a new record into Database:
PostPerRec(): httppost
Eg1:Post content body in payload in json:
[{"PerId":"8400","PerName":"Priya","PerPhone":"1234567899","PerAddress":"7 revere dr,apt B,maple shade,NJ-08052","PerCompany":"abc"}]
Eg2: With array input:
[{"PerId":"7400","PerName":"Suriya","PerPhone":"6765544467","PerAddress":"7 revere dr ","PerCompany":"xyz"},   {"PerId":"7401","PerName":"kumar","PerPhone":"1235690074","PerAddress":"6 emerson dr ","PerCompany":"def"},{"PerId":"7402","PerName":"dhars","PerPhone":"1344247768","PerAddress":"8 hamilton dr ","PerCompany":"fgh"}]
