using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvHelper
{
	public interface IContractResolver
	{
		Func<Type, bool> CanCreate { get; set; }

		Func<Type, object[], object> Create { get; set; }

		object CreateObject( Type type, object[] constructorArgs = null );
	}
}
