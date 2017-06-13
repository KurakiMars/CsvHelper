// Copyright 2009-2017 Josh Close and Contributors
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;

namespace CsvHelper.Tests.Mocks
{
	public class ParserMock : ICsvParser, IEnumerable<string[]>
	{
		private readonly Queue<string[]> rows;

		public CsvReadingContext Context { get; }

		public ICsvParserConfiguration Configuration { get; }

		public ParserMock()
		{
			Context = new CsvReadingContext( new StringReader( string.Empty ), new CsvConfiguration(), false );
			rows = new Queue<string[]>();
		}

		public ParserMock( Queue<string[]> rows )
		{
			Context = new CsvReadingContext( new StringReader( string.Empty ), new CsvConfiguration(), false );
			this.rows = rows;
		}

		public string[] Read()
		{
		    Context.Row++;
			return rows.Dequeue();
		}

		public void Add( params string[] row )
		{
			rows.Enqueue( row );
		}

		public IEnumerator<string[]> GetEnumerator()
		{
			return rows.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Dispose()
		{
		}
	}
}
