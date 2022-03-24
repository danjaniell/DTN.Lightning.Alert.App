# DTN.Lightning.Alert.App

A program that gives an alert message given a matching lightning strike event in an assets file based on quadkey location.

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
