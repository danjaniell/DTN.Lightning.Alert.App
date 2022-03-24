# DTN.Lightning.Alert.App

A .net 5 console app that gives an alert message given a matching lightning strike event in an assets file based on quadkey location.

To run:
```
cd src
dotnet run [assets json file path] [lightning strikes json file path]

e.g. dotnet run ".\Data\assets.json" ".\Data\lightning.json"
```

Sample output:
```
lightning alert for 31010:Beer Freeway
```
## More Info
 - A 'heartbeat' flashType is not a lightning strike. It should not display an alert.
 - Lat/Lon coordinates convertion is based [here](http://msdn.microsoft.com/en-us/library/bb259689.aspx).
 - Written using VS2019 and .NET 5.
 - Tests are added(xUnit). To run, simply execute command ```dotnet test```.

# Questions:

> What is the time complexity for determining if a strike has occurred for a particular asset?

Should be O(n), given that it is only looping through the given files.
> If we put this code into production, but found it too slow, or it needed to scale to many more users or more frequent strikes, what are the first things you would think of to speed it up?

By optimizing the file reading code (using a buffered stream), adding caching (we don't display subsequent strikes, might as well cache it)
