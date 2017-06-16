// Copyright 2009-2017 Josh Close and Contributors
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvHelper
{
	/// <summary>
	/// Defines context information used by the <see cref="CsvParser"/>.
	/// </summary>
	public interface IParserContext
    {
		/// <summary>
		/// Gets the character the field reader is currently on.
		/// </summary>
		int C { get; }

		/// <summary>
		/// Getsa value indicating if the field is bad.
		/// True if the field is bad, otherwise false.
		/// </summary>
		bool IsFieldBad { get; }

		/// <summary>
		/// Gets the row of the CSV file that the parser is currently on.
		/// This is the actual file row.
		/// </summary>
		int RawRow { get; }

		/// <summary>
		/// Gets the row of the CSV file that the parser is currently on.
		/// </summary>
		int Row { get; }
	}
}
