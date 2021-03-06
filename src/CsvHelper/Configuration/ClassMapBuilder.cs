﻿// Copyright 2009-2017 Josh Close and Contributors
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using System;
using System.Linq.Expressions;
using CsvHelper.TypeConversion;
using System.Collections;

namespace CsvHelper.Configuration
{
	// 1. Map
	// 2. TypeConverter
	// 3. Index
	// 4. Name
	// 5. NameIndex
	// 6. ConvertUsing
	// 7. Default
	// 8. Constant

	/// <summary>
	/// Has mapping capabilities.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	public interface IHasMap<TClass> : IBuildableClass<TClass>
	{
		/// <summary>
		/// Maps a property/field to a CSV field.
		/// </summary>
		/// <param name="expression">The property/field to map.</param>
		/// <param name="useExistingMap">If true, an existing map will be used if available.
		/// If false, a new map is created for the same property/field.</param>
		/// <returns>The property/field mapping.</returns>
		IHasMapOptions<TClass, TProperty> Map<TProperty>( Expression<Func<TClass, TProperty>> expression, bool useExistingMap = true );
	}

	/// <summary>
	/// Options after a mapping call.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasMapOptions<TClass, TProperty> :
		IHasMap<TClass>, //1
		IHasTypeConverter<TClass, TProperty>, //2
		IHasIndex<TClass, TProperty>, //3
		IHasName<TClass, TProperty>, //4
		IHasConvertUsing<TClass, TProperty>, //6
		IHasDefault<TClass, TProperty>, //7 
		IHasConstant<TClass, TProperty> //8
	{ }

	/// <summary>
	/// Has type converter capabilities.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasTypeConverter<TClass, TProperty> : IBuildableClass<TClass>
	{
		/// <summary>
		/// Specifies the <see cref="TypeConverter"/> to use
		/// when converting the property/field to and from a CSV field.
		/// </summary>
		/// <param name="typeConverter">The TypeConverter to use.</param>
		IHasTypeConverterOptions<TClass, TProperty> TypeConverter( ITypeConverter typeConverter );

		/// <summary>
		/// Specifies the <see cref="TypeConverter"/> to use
		/// when converting the property/field to and from a CSV field.
		/// </summary>
		/// <typeparam name="TConverter">The <see cref="System.Type"/> of the 
		/// <see cref="TypeConverter"/> to use.</typeparam>
		IHasTypeConverterOptions<TClass, TProperty> TypeConverter<TConverter>() where TConverter : ITypeConverter;
	}

	/// <summary>
	/// Options after a type converter call.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasTypeConverterOptions<TClass, TProperty> :
		IHasMap<TClass>, //1
		IHasDefault<TClass, TProperty> //7
	{ }

	/// <summary>
	/// Has index capabilities.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasIndex<TClass, TProperty> : IBuildableClass<TClass>
	{
		/// <summary>
		/// When reading, is used to get the field at
		/// the given index. When writing, the fields
		/// will be written in the order of the field
		/// indexes.
		/// </summary>
		/// <param name="index">The index of the CSV field.</param>
		/// <param name="indexEnd">The end index used when mapping to an <see cref="IEnumerable"/> property/field.</param>
		IHasIndexOptions<TClass, TProperty> Index( int index, int indexEnd = -1 );
	}

	/// <summary>
	/// Options after an index call.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasIndexOptions<TClass, TProperty> :
		IHasMap<TClass>, //1
		IHasTypeConverter<TClass, TProperty>, //2
		IHasName<TClass, TProperty>, //4
		IHasDefault<TClass, TProperty> //7
	{ }

	/// <summary>
	/// Has name capabilities.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasName<TClass, TProperty> : IBuildableClass<TClass>
	{
		/// <summary>
		/// When reading, is used to get the field
		/// at the index of the name if there was a
		/// header specified. It will look for the
		/// first name match in the order listed.
		/// When writing, sets the name of the 
		/// field in the header record.
		/// The first name will be used.
		/// </summary>
		/// <param name="names">The possible names of the CSV field.</param>
		IHasNameOptions<TClass, TProperty> Name( params string[] names );
	}

	/// <summary>
	/// Options after a name call.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasNameOptions<TClass, TProperty> :
		IHasMap<TClass>, //1
		IHasTypeConverter<TClass, TProperty>, //2
		IHasNameIndex<TClass, TProperty>, //5
		IHasDefault<TClass, TProperty> //7
	{ }

	/// <summary>
	/// Has name index capabilities.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasNameIndex<TClass, TProperty> : IBuildableClass<TClass>
	{
		/// <summary>
		/// When reading, is used to get the 
		/// index of the name used when there 
		/// are multiple names that are the same.
		/// </summary>
		/// <param name="index">The index of the name.</param>
		IHasNameIndexOptions<TClass, TProperty> NameIndex( int index );
	}

	/// <summary>
	/// Options after a name index call.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasNameIndexOptions<TClass, TProperty> :
		IHasMap<TClass>, //1
		IHasTypeConverter<TClass, TProperty>, //2
		IHasDefault<TClass, TProperty> //7
	{ }

	/// <summary>
	/// Has convert using capabilities.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasConvertUsing<TClass, TProperty> : IBuildableClass<TClass>
	{
		/// <summary>
		/// Specifies an expression to be used to convert data in the
		/// row to the property/field.
		/// </summary>
		/// <param name="convertExpression">The convert expression.</param>
		IHasMap<TClass> ConvertUsing( Func<IReaderRow, TProperty> convertExpression );

		/// <summary>
		/// Specifies an expression to be used to convert the object
		/// to a field.
		/// </summary>
		/// <param name="convertExpression">The convert expression.</param>
		IHasMap<TClass> ConvertUsing( Func<TClass, string> convertExpression );
	}

	/// <summary>
	/// Has default capabilities.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasDefault<TClass, TProperty> : IBuildableClass<TClass>
	{
		/// <summary>
		/// The default value that will be used when reading when
		/// the CSV field is empty.
		/// </summary>
		/// <param name="defaultValue">The default value.</param>
		IHasMap<TClass> Default( TProperty defaultValue );

		/// <summary>
		/// The default value that will be used when reading when
		/// the CSV field is empty. This value is not type checked
		/// and will use a <see cref="ITypeConverter"/> to convert
		/// the field. This could potentially have runtime errors.
		/// </summary>
		/// <param name="defaultValue">The default value.</param>
		IHasMap<TClass> Default( string defaultValue );
	}

	/// <summary>
	/// Has constant capabilities.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	/// <typeparam name="TProperty">The property type.</typeparam>
	public interface IHasConstant<TClass, TProperty> : IBuildableClass<TClass>
	{
		/// <summary>
		/// The constant value that will be used for every record when 
		/// reading and writing. This value will always be used no matter 
		/// what other mapping configurations are specified.
		/// </summary>
		/// <param name="value">The constant value.</param>
		IHasMap<TClass> Constant( TProperty value );
	}

	/// <summary>
	/// Has build capabilities.
	/// </summary>
	/// <typeparam name="TClass">The class type.</typeparam>
	public interface IBuildableClass<TClass>
	{
		/// <summary>
		/// Builds the <see cref="CsvClassMap{TClass}"/>.
		/// </summary>
		CsvClassMap<TClass> Build();
	}

	internal class ClassMapBuilder<TClass> : IHasMap<TClass>
	{
		private readonly CsvClassMap<TClass> map;

		public ClassMapBuilder()
		{
			map = new BuilderClassMap<TClass>();
		}

		public IHasMapOptions<TClass, TProperty> Map<TProperty>( Expression<Func<TClass, TProperty>> expression, bool useExistingMap = true )
		{
			return new PropertyMapBuilder<TClass, TProperty>( map, map.Map( expression, useExistingMap ) );
		}

		public CsvClassMap<TClass> Build()
		{
			return map;
		}

		private class BuilderClassMap<T> : CsvClassMap<T> { }
	}

	internal class PropertyMapBuilder<TClass, TProperty> :
		IHasMap<TClass>, //1
		IHasMapOptions<TClass, TProperty>, //1 result
		IHasTypeConverter<TClass, TProperty>, //2
		IHasTypeConverterOptions<TClass, TProperty>,//2 result
		IHasIndex<TClass, TProperty>, //3
		IHasIndexOptions<TClass, TProperty>, //3 result
		IHasName<TClass, TProperty>, //4
		IHasNameOptions<TClass, TProperty>, //4 result
		IHasNameIndex<TClass, TProperty>, //5
		IHasNameIndexOptions<TClass, TProperty>, //5 result
		IHasConvertUsing<TClass, TProperty>, //6 - goes back to 1 only
		IHasDefault<TClass, TProperty>, //7 - goes back to 1 only
		IHasConstant<TClass, TProperty>
	{
		private readonly CsvClassMap<TClass> classMap;
		private readonly CsvPropertyMap<TClass, TProperty> propertyMap;

		public PropertyMapBuilder( CsvClassMap<TClass> classMap, CsvPropertyMap<TClass, TProperty> propertyMap )
		{
			this.classMap = classMap;
			this.propertyMap = propertyMap;
		}

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
		public IHasMapOptions<TClass, TProperty> Map<TProperty>( Expression<Func<TClass, TProperty>> expression, bool useExistingMap = true )
		{
			return new PropertyMapBuilder<TClass, TProperty>( classMap, classMap.Map( expression, useExistingMap ) );
		}
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type

		public IHasMap<TClass> ConvertUsing( Func<IReaderRow, TProperty> convertExpression )
		{
			propertyMap.ConvertUsing( convertExpression );
			return this;
		}

		public IHasMap<TClass> ConvertUsing( Func<TClass, string> convertExpression )
		{
			propertyMap.ConvertUsing( convertExpression );
			return this;
		}

		public IHasMap<TClass> Default( TProperty defaultValue )
		{
			propertyMap.Default( defaultValue );
			return this;
		}

		public IHasMap<TClass> Default( string defaultValue )
		{
			propertyMap.Default( defaultValue );
			return this;
		}

		public IHasIndexOptions<TClass, TProperty> Index( int index, int indexEnd = -1 )
		{
			propertyMap.Index( index, indexEnd );
			return this;
		}

		public IHasNameOptions<TClass, TProperty> Name( params string[] names )
		{
			propertyMap.Name( names );
			return this;
		}

		public IHasNameIndexOptions<TClass, TProperty> NameIndex( int index )
		{
			propertyMap.NameIndex( index );
			return this;
		}

		public IHasTypeConverterOptions<TClass, TProperty> TypeConverter( ITypeConverter typeConverter )
		{
			propertyMap.TypeConverter( typeConverter );
			return this;
		}

		public IHasTypeConverterOptions<TClass, TProperty> TypeConverter<TConverter>() where TConverter : ITypeConverter
		{
			propertyMap.TypeConverter<TConverter>();
			return this;
		}

		public IHasMap<TClass> Constant( TProperty value )
		{
			propertyMap.Constant( value );
			return this;
		}

		public CsvClassMap<TClass> Build()
		{
			return classMap;
		}
	}
}