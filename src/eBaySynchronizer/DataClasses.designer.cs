﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eBaySynchronizer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="Northwind")]
	public partial class eBayDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertebay_Category(ebay_Category instance);
    partial void Updateebay_Category(ebay_Category instance);
    partial void Deleteebay_Category(ebay_Category instance);
    #endregion
		
		public eBayDataContext() : 
				base(global::eBaySynchronizer.Properties.Settings.Default.eBayConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public eBayDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public eBayDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public eBayDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public eBayDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<ebay_Category> ebay_Categories
		{
			get
			{
				return this.GetTable<ebay_Category>();
			}
		}
	}
	
	[Table(Name="dbo.ebay_Category")]
	public partial class ebay_Category : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CategoryID;
		
		private int _Level;
		
		private string _Name;
		
		private System.Nullable<int> _ParentID;
		
		private bool _Leaf;
		
		private bool _Expired;
		
		private bool _Virtual;
		
		private EntitySet<ebay_Category> _ChildCategories;
		
		private EntityRef<ebay_Category> _ParentCategory;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCategoryIDChanging(int value);
    partial void OnCategoryIDChanged();
    partial void OnLevelChanging(int value);
    partial void OnLevelChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnParentIDChanging(System.Nullable<int> value);
    partial void OnParentIDChanged();
    partial void OnLeafChanging(bool value);
    partial void OnLeafChanged();
    partial void OnExpiredChanging(bool value);
    partial void OnExpiredChanged();
    partial void OnVirtualChanging(bool value);
    partial void OnVirtualChanged();
    #endregion
		
		public ebay_Category()
		{
			this._ChildCategories = new EntitySet<ebay_Category>(new Action<ebay_Category>(this.attach_ChildCategories), new Action<ebay_Category>(this.detach_ChildCategories));
			this._ParentCategory = default(EntityRef<ebay_Category>);
			OnCreated();
		}
		
		[Column(Storage="_CategoryID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int CategoryID
		{
			get
			{
				return this._CategoryID;
			}
			set
			{
				if ((this._CategoryID != value))
				{
					this.OnCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._CategoryID = value;
					this.SendPropertyChanged("CategoryID");
					this.OnCategoryIDChanged();
				}
			}
		}
		
		[Column(Name="[Level]", Storage="_Level", DbType="Int NOT NULL")]
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if ((this._Level != value))
				{
					this.OnLevelChanging(value);
					this.SendPropertyChanging();
					this._Level = value;
					this.SendPropertyChanged("Level");
					this.OnLevelChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(30) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_ParentID", DbType="Int")]
		public System.Nullable<int> ParentID
		{
			get
			{
				return this._ParentID;
			}
			set
			{
				if ((this._ParentID != value))
				{
					if (this._ParentCategory.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnParentIDChanging(value);
					this.SendPropertyChanging();
					this._ParentID = value;
					this.SendPropertyChanged("ParentID");
					this.OnParentIDChanged();
				}
			}
		}
		
		[Column(Storage="_Leaf", DbType="Bit NOT NULL")]
		public bool Leaf
		{
			get
			{
				return this._Leaf;
			}
			set
			{
				if ((this._Leaf != value))
				{
					this.OnLeafChanging(value);
					this.SendPropertyChanging();
					this._Leaf = value;
					this.SendPropertyChanged("Leaf");
					this.OnLeafChanged();
				}
			}
		}
		
		[Column(Storage="_Expired", DbType="Bit NOT NULL")]
		public bool Expired
		{
			get
			{
				return this._Expired;
			}
			set
			{
				if ((this._Expired != value))
				{
					this.OnExpiredChanging(value);
					this.SendPropertyChanging();
					this._Expired = value;
					this.SendPropertyChanged("Expired");
					this.OnExpiredChanged();
				}
			}
		}
		
		[Column(Storage="_Virtual", DbType="Bit NOT NULL")]
		public bool Virtual
		{
			get
			{
				return this._Virtual;
			}
			set
			{
				if ((this._Virtual != value))
				{
					this.OnVirtualChanging(value);
					this.SendPropertyChanging();
					this._Virtual = value;
					this.SendPropertyChanged("Virtual");
					this.OnVirtualChanged();
				}
			}
		}
		
		[Association(Name="ebay_Category_ebay_Category", Storage="_ChildCategories", ThisKey="CategoryID", OtherKey="ParentID")]
		public EntitySet<ebay_Category> ChildCategories
		{
			get
			{
				return this._ChildCategories;
			}
			set
			{
				this._ChildCategories.Assign(value);
			}
		}
		
		[Association(Name="ebay_Category_ebay_Category", Storage="_ParentCategory", ThisKey="ParentID", OtherKey="CategoryID", IsForeignKey=true)]
		public ebay_Category ParentCategory
		{
			get
			{
				return this._ParentCategory.Entity;
			}
			set
			{
				ebay_Category previousValue = this._ParentCategory.Entity;
				if (((previousValue != value) 
							|| (this._ParentCategory.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ParentCategory.Entity = null;
						previousValue.ChildCategories.Remove(this);
					}
					this._ParentCategory.Entity = value;
					if ((value != null))
					{
						value.ChildCategories.Add(this);
						this._ParentID = value.CategoryID;
					}
					else
					{
						this._ParentID = default(Nullable<int>);
					}
					this.SendPropertyChanged("ParentCategory");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_ChildCategories(ebay_Category entity)
		{
			this.SendPropertyChanging();
			entity.ParentCategory = this;
		}
		
		private void detach_ChildCategories(ebay_Category entity)
		{
			this.SendPropertyChanging();
			entity.ParentCategory = null;
		}
	}
}
#pragma warning restore 1591
