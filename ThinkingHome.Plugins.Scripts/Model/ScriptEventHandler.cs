using System;

namespace ThinkingHome.Plugins.Scripts.Model;

public class ScriptEventHandler {
    public Guid Id { get; init; }

    public string EventAlias { get; init; }

    public Guid UserScriptId { get; init; }
    public UserScript UserScript { get; init; }
}