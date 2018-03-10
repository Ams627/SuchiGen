using System;
using System.Collections.Generic;
using System.Linq.Expressions;

class Program
{
    static void Main()
    {
        var source = Expression.Parameter(typeof(Test), "source");
        var target = Expression.Parameter(typeof(Test), "target");
        var properties = typeof(Test).GetProperties();
        var assignments = new List<Expression>(properties.Length);
        foreach (var prop in properties)
        {
            var sourceProp = Expression.Property(source, prop);
            var targetProp = Expression.Property(target, prop);
            var assignment = Expression.Assign(targetProp, sourceProp);
            assignments.Add(assignment);
        }
        var body = Expression.Block(assignments);
        var lambda = Expression.Lambda<Action<Test, Test>>(body, source, target);
        Action<Test, Test> mapper = lambda.Compile();
        var s = new Test { Name = "Tester" };
        var t = new Test();
        mapper(s, t);
        Console.WriteLine();

    }
}

class Test
{
    public string Name { get; set; }
}