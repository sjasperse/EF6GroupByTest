# EF6 Group By Test

Demonstration showing how to do group by on dynamic fields in EF Core 6.

## Output

```
% dotnet run
EXPRESSIONS:
    GroupBy: new(Region)
    Select: new(Key.Region, Sum(OrderVolume) AS OrderVolume)

EF LOG:
info: 3/22/2022 13:55:37.655 CoreEventId.ContextInitialized[10403] (Microsoft.EntityFrameworkCore.Infrastructure) 
      Entity Framework Core 6.0.3 initialized 'AppDbContext' using provider 'Microsoft.EntityFrameworkCore.SqlServer:6.0.3' with options: None
info: 3/22/2022 13:55:38.275 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (19ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [c].[Region], COALESCE(SUM([c].[OrderVolume]), 0.0) AS [OrderVolume]
      FROM [Customers] AS [c]
      GROUP BY [c].[Region]

RESULTS:
[
  {
    "Region": "EU",
    "OrderVolume": 300000.0000
  },
  {
    "Region": "NA",
    "OrderVolume": 200000.0000
  }
]
```

