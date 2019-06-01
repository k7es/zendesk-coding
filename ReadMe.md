
# Zendesk coding challenge
A simple .NET Core console app that searches users, organisation and tickets.

### Usage

To build

>cd ConsoleSearch <br>  dotnet build ConsoleSearch.csproj

To run

>cd ConsoleSearch <br>  dotnet run ConsoleSearch.csproj

### Assumptions
- All data items have a unique ID
- Local console app
- `organization_id` property in both the data for Tickets and Users is related to the  `_id` property in Organizations and not the `external_id` GUID
- Data input can be case insensitive
- Order of the results does not matter
- Prioritied searching by `id`
- If data adds new fields or new data types, the data models will need to be changed. 

### Trade Offs
- An initial load up time while the app structures the data.
- There is heavy memeory usage because of the use of reflection to produce a generic `Search` class.
- For all searches other than by `id`, the app searches using LINQ which is not the most performant technique but for the amount of data that will fit in memory, it should be good enough to do an efficient search and for readability. 
