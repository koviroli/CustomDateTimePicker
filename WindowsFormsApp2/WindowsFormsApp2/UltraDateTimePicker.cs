﻿using System;

    /// <summary>
        //Format and CustomForamt are shadowed since base.Format is always Custom
        //and base.CustomFormat is used in setFormat to show the intended _Format
        //You have to keep base.Format set to Custom to avoid superfluous ValueChanged
        //events from occuring.
        private DateTimePickerFormat _Format; // Variable to store 'Format'
        private string _CustomFormat; //Variable to store 'CustomFormat'
        private string _nullText = "";  //Variable to store null Display Text

        private bool _readOnly = false;//Flag to denote the UDTP is in ReadOnly Mode
        private bool _visible = true; //Overridden to show the proper Display for Readonly Mode
        private bool _tabStopWhenReadOnly = false; //Variable to store whether or not the UDTP is a TabStop when in ReadOnly Mode
        private TextBox _textBox;//TextBox Decorated when in ReadOnly Mode

        /// <summary>
        public UltraDateTimePicker()
            initializing = true;
            initializing = false;
            //I added special logic here to handle positioning the _TextBox when the UDTP is in a FlowLayoutPanel
            else if (this.Parent.GetType() == typeof(FlowLayoutPanel))
            {
            //I use the following block of code to walk up the parent-child
            //chain and find the first member that has a Load event that I can attach to
            //I set the visiblilty during this event so that Databinding will work correctly
            //otherwise the UDTP will fail to bind properly if its visibility is false during the
            //Load event.(Strange but true, has to do with hidden controls not binding for performance reasons)
            Control parent = this;

        /// <summary>
                //if (this.MinDate == base.Value) //Check to see if set to MinDate(null), return null or base.Value accordingly
                if (_isNull)
                {
                    {
                {
                                                                 //(null->value needs a value changed even though base.Value did not change)
                    {
                    {
                _textBox.Text = this.Text;

        /// <summary>

        /// <summary>
            if (_isNull)//If null apply NullText to the UDTP
            {
                //The Following is used to get a string representation ot the current UDTP Format
                //And then set the CustomFormat to match the intended format
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;

        /// <summary>

        /// <summary>
        [Browsable(true)]

        /// <summary>
            }

        /// <summary>

        /// <summary>

        /// <summary>
        protected override void WndProc(ref Message m)
            }

        ///<summary>

        /// <summary>

        /// <summary>
            setVisibility(); //Reset Visibilty for new parent
        }

        /// <summary>

        /// <summary>

        /// <summary>

        /// <summary>

        /// <summary>

        /// <summary>

        /// <summary>

        ////// <summary>
        /// Propagates Font to the _TextBox

        /// <summary>
                return;
                else
            }
            }
            {
            {
                //I Added Special logic here to handle positioning the _TextBox when the UDTP is in a TableLayoutPanel
                if (this.Parent.GetType() == typeof(TableLayoutPanel))
                //I added special logic here to handle positioning the _TextBox when the UDTP is in a FlowLayoutPanel
                else if (this.Parent.GetType() == typeof(FlowLayoutPanel))
                {