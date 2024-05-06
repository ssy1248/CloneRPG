using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterCommand
{
    public interface ICommandHandle
    {
        public void ProcessCommand(Command command);
    }
}
