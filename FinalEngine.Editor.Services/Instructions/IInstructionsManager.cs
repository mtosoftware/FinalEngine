namespace FinalEngine.Editor.Services.Instructions
{
    using System;

    public interface IInstructionsManager
    {
        event EventHandler InstructionsModified;

        bool CanRedo { get; }

        bool CanUndo { get; }

        void AddInstruction(IInstruction instruction);

        void Redo();

        void Undo();
    }
}
