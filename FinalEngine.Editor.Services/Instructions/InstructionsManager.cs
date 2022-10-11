namespace FinalEngine.Editor.Services.Instructions
{
    using System;
    using System.Collections.Generic;

    public class InstructionsManager : IInstructionsManager
    {
        private readonly Stack<IInstruction> redoInstructions;

        private readonly Stack<IInstruction> undoInstructions;

        public InstructionsManager()
        {
            this.undoInstructions = new Stack<IInstruction>();
            this.redoInstructions = new Stack<IInstruction>();
        }

        public event EventHandler InstructionsModified;

        public bool CanRedo
        {
            get { return this.redoInstructions.Count > 0; }
        }

        public bool CanUndo
        {
            get { return this.undoInstructions.Count > 0; }
        }

        public void AddInstruction(IInstruction instruction)
        {
            this.undoInstructions.Push(instruction);
            this.redoInstructions.Clear();

            this.InstructionsModified?.Invoke(this, EventArgs.Empty);
        }

        public void Redo()
        {
            if (!this.redoInstructions.TryPop(out var instruction))
            {
                return;
            }

            this.undoInstructions.Push(instruction);
            this.InstructionsModified?.Invoke(this, EventArgs.Empty);

            instruction.Execute();
        }

        public void Undo()
        {
            if (!this.undoInstructions.TryPop(out var instruction))
            {
                return;
            }

            this.redoInstructions.Push(instruction);
            this.InstructionsModified?.Invoke(this, EventArgs.Empty);

            instruction.UnExecute();
        }
    }
}
