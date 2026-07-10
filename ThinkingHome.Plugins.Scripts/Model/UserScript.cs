using System;

namespace ThinkingHome.Plugins.Scripts.Model;

public class UserScript {
    public Guid Id { get; init; }

    public string Name { get; set; }

    public string Body { get; set; }
}