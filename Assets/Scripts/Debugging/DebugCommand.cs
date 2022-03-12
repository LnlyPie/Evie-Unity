using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DebugCommandBase : MonoBehaviour
{
    private string _commandId;
    private string _commandDesc;
    private string _commandForm;

    public string commandId { get { return _commandId; } }
    public string commandDesc { get { return _commandDesc; } }
    public string commandForm { get { return _commandForm; } }

    public DebugCommandBase(string id, string desc, string form) {
        _commandId = id;
        _commandDesc = desc;
        _commandForm = form;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action command;

    public DebugCommand(string id, string desc, string form, Action command) : base (id, desc, form) {
        this.command = command;
    }

    public void Invoke() {
        command.Invoke();
    }
}

public class DebugCommand<T1> : DebugCommandBase
{
    private Action<T1> command;

    public DebugCommand(string id, string desc, string form, Action<T1> command) : base(id, desc, form) {
        this.command = command;
    }

    public void Invoke(T1 value) {
        command.Invoke(value);
    }
}
