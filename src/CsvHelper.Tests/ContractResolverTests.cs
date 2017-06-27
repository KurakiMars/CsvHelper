using CsvHelper.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvHelper.Tests
{
	[TestClass]
	public class ContractResolverTests
	{
		[TestCleanup]
		public void Cleanup()
		{
			ContractResolver.Current = new ContractResolver( type => true, ReflectionHelper.CreateInstanceWithoutContractResolver );
		}

		[TestMethod]
		public void InterfaceReferenceMappingTest()
		{
			using( var stream = new MemoryStream() )
			using( var writer = new StreamWriter( stream ) )
			using( var reader = new StreamReader( stream ) )
			using( var csv = new CsvReader( reader ) )
			{
				writer.WriteLine( "AId,BId,CId,DId" );
				writer.WriteLine( "1,2,3,4" );
				writer.Flush();
				stream.Position = 0;

				ContractResolver.Current = new TestContractResolver();

				csv.Configuration.RegisterClassMap<AMap>();
				var records = csv.GetRecords<IA>().ToList();
			}
		}

		[TestMethod]
		public void InterfacePropertySubMappingTest()
		{
			using( var stream = new MemoryStream() )
			using( var writer = new StreamWriter( stream ) )
			using( var reader = new StreamReader( stream ) )
			using( var csv = new CsvReader( reader ) )
			{
				writer.WriteLine( "AId,BId,CId,DId" );
				writer.WriteLine( "1,2,3,4" );
				writer.Flush();
				stream.Position = 0;

				ContractResolver.Current = new TestContractResolver();

				csv.Configuration.RegisterClassMap<ASubPropertyMap>();
				var records = csv.GetRecords<IA>().ToList();
			}
		}

		[TestMethod]
		public void InterfaceAutoMappingTest()
		{
			using( var stream = new MemoryStream() )
			using( var writer = new StreamWriter( stream ) )
			using( var reader = new StreamReader( stream ) )
			using( var csv = new CsvReader( reader ) )
			{
				writer.WriteLine( "AId,BId,CId,DId" );
				writer.WriteLine( "1,2,3,4" );
				writer.Flush();
				stream.Position = 0;

				ContractResolver.Current = new TestContractResolver();

				var records = csv.GetRecords<IA>().ToList();
			}
		}

		private class TestContractResolver : IContractResolver
		{
			public Func<Type, bool> CanCreate { get; set; }

			public Func<Type, object[], object> Create { get; set; }

			public object CreateObject( Type type, object[] constructorArgs = null )
			{
				if( type == typeof( IA ) )
				{
					return new A();
				}

				if( type == typeof( IB ) )
				{
					return new B();
				}

				if( type == typeof( IC ) )
				{
					return new C();
				}

				if( type == typeof( ID ) )
				{
					return new D();
				}

				return ReflectionHelper.CreateInstanceWithoutContractResolver( type, constructorArgs );
			}
		}

		private interface IA
		{
			int AId { get; set; }

			IB B { get; set; }
		}

		private interface IB
		{
			int BId { get; set; }

			IC C { get; set; }
		}

		private interface IC
		{
			int CId { get; set; }

			ID D { get; set; }
		}

		private interface ID
		{
			int DId { get; set; }
		}

		private class A : IA
		{
			public int AId { get; set; }

			public IB B { get; set; }
		}

		private class B : IB
		{
			public int BId { get; set; }

			public IC C { get; set; }
		}

		private class C : IC
		{
			public int CId { get; set; }

			public ID D { get; set; }
		}

		private class D : ID
		{
			public int DId { get; set; }
		}

		private sealed class ASubPropertyMap : CsvClassMap<IA>
		{
			public ASubPropertyMap()
			{
				Map( m => m.AId );
				Map( m => m.B.BId );
				Map( m => m.B.C.CId );
				Map( m => m.B.C.D.DId );
			}
		}

		private sealed class AMap : CsvClassMap<IA>
		{
			public AMap()
			{
				Map( m => m.AId );
				References<BMap>( m => m.B );
			}
		}

		private sealed class BMap : CsvClassMap<IB>
		{
			public BMap()
			{
				Map( m => m.BId );
				References<CMap>( m => m.C );
			}
		}

		private sealed class CMap : CsvClassMap<IC>
		{
			public CMap()
			{
				Map( m => m.CId );
				References<DMap>( m => m.D );
			}
		}

		private sealed class DMap : CsvClassMap<ID>
		{
			public DMap()
			{
				Map( m => m.DId );
			}
		}
	}
}
