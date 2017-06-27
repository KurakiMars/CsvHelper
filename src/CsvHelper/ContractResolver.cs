using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvHelper
{
	public class ContractResolver : IContractResolver
	{
		private static readonly object locker = new object();
		private static IContractResolver current = new ContractResolver( type => true, ReflectionHelper.CreateInstanceWithoutContractResolver );

		public Func<Type, bool> CanCreate { get; set; }

		public Func<Type, object[], object> Create { get; set; }

		public ContractResolver() : this( t => true, ReflectionHelper.CreateInstance ) { }

		public ContractResolver( Func<Type, bool> canCreate, Func<Type, object[], object> create )
		{
			CanCreate = canCreate;
			Create = create;
		}

		public object CreateObject( Type type, object[] constructorArgs = null )
		{
			if( CanCreate == null || CanCreate( type ) )
			{
				return Create( type, constructorArgs );
			}

			return null;
		}

		public static IContractResolver Current
		{
			get
			{
				lock( locker )
				{
					return current;
				}
			}
			set
			{
				lock( locker )
				{
					current = value;
				}
			}
		}
	}
}
