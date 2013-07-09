﻿using System;
using System.Text;

namespace Alicargo.DataAccess.DbContext
{
	internal class DebugTextWriter : System.IO.TextWriter
	{
		public override void Write(char[] buffer, int index, int count)
		{
			System.Diagnostics.Debug.Write(new String(buffer, index, count));
		}

		public override void Write(string value)
		{
			System.Diagnostics.Debug.Write(value);
		}

		public override Encoding Encoding
		{
			get { return Encoding.Default; }
		}
	}
}