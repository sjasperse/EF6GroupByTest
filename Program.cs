using EF6GroupBy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using System.Linq.Dynamic.Core;
// ^ the magic is in this nuget package
// https://github.com/zzzprojects/System.Linq.Dynamic.Core
// https://dynamic-linq.net/overview

var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer($"Server=.;Database=EF6GroupByTest;User Id=sa;pwd={System.Environment.GetEnvironmentVariable("SA_PASSWORD")}")
    .LogTo(Console.WriteLine, LogLevel.Information)
    .Options;

var context = new AppDbContext(contextOptions);

var columns = new [] {
    new Column("Region"),
    new Column("OrderVolume", "Sum(OrderVolume)")
};
var groupByColumns = columns.Where(x => x.AggregateExp == null);
var aggregateColumns = columns.Where(x => x.AggregateExp != null);

var groupByExp = $"new({string.Join(", ", groupByColumns.Select(x => x.Name))})";
var selectExp = $"new({string.Join(", ", groupByColumns.Select(x => "Key." + x.Name))}, {string.Join(", ", aggregateColumns.Select(x => $"{x.AggregateExp} AS {x.Name}"))})";

Console.WriteLine("EXPRESSIONS:");
Console.WriteLine("    GroupBy: " + groupByExp);
Console.WriteLine("    Select: " + selectExp);
Console.WriteLine();

Console.WriteLine("EF LOG:");
var query = context.Customers
    .GroupBy(groupByExp)
    .Select(selectExp)
    .ToDynamicArray();

Console.WriteLine();
Console.WriteLine("RESULTS:");
Console.WriteLine(JsonConvert.SerializeObject(query, Formatting.Indented));

record Column(string Name, string AggregateExp = null);
