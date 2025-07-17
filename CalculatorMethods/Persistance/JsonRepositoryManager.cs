using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using CalculatorProject.Contracts;
using UnitsNet;

namespace CalculatorProject.Persistance;

public class JsonRepositoryManager : IRepository
{
    public List<MathLogItem> Memory { get; } = new List<MathLogItem>();
    public void Save(string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Convert MathLogItem to MathLogEntity
        var entities = Memory.ToEntities();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        var json = JsonSerializer.Serialize(entities, options);
        File.WriteAllText(filePath, json);
    }

    public void Load(string filePath)
    {
        if (!File.Exists(filePath))
            Memory.AddRange(new List<MathLogItem>());

        var json = File.ReadAllText(filePath);

        var option = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        var entities = JsonSerializer.Deserialize<List<MathLogEntity>>(json);

        if (entities == null)
        {
            Memory.AddRange(new List<MathLogItem>());
        }
        else
        {
            // Convert MathLogEntity to MathLogItem
            var result = entities.Select(e => e.FromEntity()).ToList();

            Memory.AddRange(result);
        }
    }
}
