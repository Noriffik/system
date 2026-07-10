using System;

namespace ThinkingHome.Plugins.Scripts.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ScriptCommandAttribute(string alias) : Attribute {
    public string Alias { get; } = alias;
}
