﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CsvHelper.Configuration
{
	/// <summary>
	/// Mapping info for a reference parameter mapping to a class.
	/// </summary>
	public class CsvParameterReferenceMap
    {
		private readonly CsvParameterReferenceMapData data;

		/// <summary>
		/// Gets the parameter reference map data.
		/// </summary>
		public CsvParameterReferenceMapData Data => data;

		/// <summary>
		/// Initializes a new instance of the <see cref="CsvParameterReferenceMap"/> class.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="mapping">The <see cref="CsvClassMap"/> to use for the reference map.</param>
		public CsvParameterReferenceMap( ParameterInfo parameter, CsvClassMap mapping )
		{
			if( mapping == null )
			{
				throw new ArgumentNullException( nameof( mapping ) );
			}

			data = new CsvParameterReferenceMapData( parameter, mapping );
		}

		/// <summary>
		/// Appends a prefix to the header of each field of the reference parameter.
		/// </summary>
		/// <param name="prefix">The prefix to be prepended to headers of each reference parameter.</param>
		/// <returns>The current <see cref="CsvParameterReferenceMap" /></returns>
		public CsvParameterReferenceMap Prefix( string prefix = null )
		{
			if( string.IsNullOrEmpty( prefix ) )
			{
				prefix = data.Parameter.Name + ".";
			}

			data.Prefix = prefix;

			return this;
		}

		/// <summary>
		/// Get the largest index for the
		/// properties/fields and references.
		/// </summary>
		/// <returns>The max index.</returns>
		internal int GetMaxIndex()
		{
			return data.Mapping.GetMaxIndex();
		}
	}
}
