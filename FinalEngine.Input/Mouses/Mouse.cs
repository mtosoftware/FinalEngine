// <copyright file="Mouse.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IMouse"/>, that interfaces with an <see cref="IMouseDevice"/>.
    /// </summary>
    /// <seealso cref="IMouse"/>
    public class Mouse : IMouse, IDisposable
    {
        /// <summary>
        ///   The initial size capacity of the <see cref="buttonsDown"/> and <see cref="buttonsDownLast"/> collections.
        /// </summary>
        private const int InitialSizeCapacity = 12;

        /// <summary>
        ///   The buttons down during the current frame.
        /// </summary>
        private readonly IList<MouseButton> buttonsDown;

        /// <summary>
        ///   The physical mouse device.
        /// </summary>
        private readonly IMouseDevice? device;

        /// <summary>
        ///   The buttons down during the previous frame.
        /// </summary>
        private IList<MouseButton> buttonsDownLast;

        /// <summary>
        ///   The cursor location as of the last time <see cref="IMouseDevice.Move"/> was raised.
        /// </summary>
        private PointF location;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Mouse"/> class.
        /// </summary>
        /// <param name="device">
        ///   Specifies a <see cref="IMouseDevice"/> that represents the mouse device to listen to.
        /// </param>
        public Mouse(IMouseDevice? device)
        {
            this.device = device;

            this.buttonsDown = new List<MouseButton>(InitialSizeCapacity);
            this.buttonsDownLast = new List<MouseButton>(InitialSizeCapacity);

            if (this.device != null)
            {
                this.device.ButtonDown += this.Device_ButtonDown;
                this.device.ButtonUp += this.Device_ButtonUp;
                this.device.Move += this.Device_Move;
                this.device.Scroll += this.Device_Scroll;
            }
        }

        [ExcludeFromCodeCoverage]
        ~Mouse()
        {
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public PointF Delta
        {
            get { return this.device == null ? PointF.Empty : this.device.LocationDelta; }
        }

        /// <inheritdoc/>
        public PointF Location
        {
            get
            {
                return this.location;
            }

            set
            {
                if (this.device != null)
                {
                    if (this.Location.Equals(value))
                    {
                        return;
                    }

                    this.device.SetCursorLocation(value);
                }
            }
        }

        /// <inheritdoc/>
        public double WheelOffset { get; private set; }

        protected bool IsDisposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public bool IsButtonDown(MouseButton button)
        {
            return this.buttonsDown.Contains(button);
        }

        /// <inheritdoc/>
        public bool IsButtonPressed(MouseButton button)
        {
            return this.buttonsDown.Contains(button) && !this.buttonsDownLast.Contains(button);
        }

        /// <inheritdoc/>
        public bool IsButtonReleased(MouseButton button)
        {
            return !this.buttonsDown.Contains(button) && this.buttonsDownLast.Contains(button);
        }

        /// <inheritdoc/>
        /// <remarks>
        ///   This method should only be called after the user has checked for input state changes.
        /// </remarks>
        public void Update()
        {
            this.buttonsDownLast = new List<MouseButton>(this.buttonsDown);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (this.device != null)
            {
                this.device.ButtonDown -= this.Device_ButtonDown;
                this.device.ButtonUp -= this.Device_ButtonUp;
                this.device.Move -= this.Device_Move;
                this.device.Scroll -= this.Device_Scroll;
            }

            this.IsDisposed = true;
        }

        /// <summary>
        ///   Handles the <see cref="IMouseDevice.ButtonDown"/> event.
        /// </summary>
        /// <param name="sender">
        ///   The sender.
        /// </param>
        /// <param name="e">
        ///   The <see cref="MouseButtonEventArgs"/> instance containing the event data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="e"/> parameter is null.
        /// </exception>
        private void Device_ButtonDown(object? sender, MouseButtonEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            this.buttonsDown.Add(e.Button);
        }

        /// <summary>
        ///   Handles the <see cref="IMouseDevice.ButtonUp"/> event.
        /// </summary>
        /// <param name="sender">
        ///   The sender.
        /// </param>
        /// <param name="e">
        ///   The <see cref="MouseButtonEventArgs"/> instance containing the event data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="e"/> parameter is null.
        /// </exception>
        private void Device_ButtonUp(object? sender, MouseButtonEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            while (this.buttonsDown.Contains(e.Button))
            {
                this.buttonsDown.Remove(e.Button);
            }
        }

        /// <summary>
        ///   Handles the <see cref="IMouseDevice.Move"/> event.
        /// </summary>
        /// <param name="sender">
        ///   The sender.
        /// </param>
        /// <param name="e">
        ///   The <see cref="MouseMoveEventArgs"/> instance containing the event data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="e"/> parameter is null.
        /// </exception>
        private void Device_Move(object? sender, MouseMoveEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            this.location = e.Location;
        }

        /// <summary>
        ///   Handles the <see cref="IMouseDevice.Scroll"/> event.
        /// </summary>
        /// <param name="sender">
        ///   The sender.
        /// </param>
        /// <param name="e">
        ///   The <see cref="MouseScrollEventArgs"/> instance containing the event data.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="e"/> parameter is null.
        /// </exception>
        private void Device_Scroll(object? sender, MouseScrollEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            this.WheelOffset = e.Offset;
        }
    }
}
