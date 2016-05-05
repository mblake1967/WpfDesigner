using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.WpfDesign.Designer;
using ICSharpCode.WpfDesign.Designer.PropertyGrid;
using ICSharpCode.WpfDesign;
using ICSharpCode.WpfDesign.Designer.OutlineView;
using System.Windows;
using System.ComponentModel;

namespace MyTestAssembly
{
	//Similar to the Shell class used in the sharpdevlop large sample
	public class MyDesignerModel : INotifyPropertyChanged
	{
		public MyDesignerModel()
		{
			this.m_designSurface = new DesignSurface();

			
						
		}

		//public void SubscribeToComponentEvents()
		//{
		//	this.m_designSurface.DesignContext.Services.Component.ComponentRegisteredAndAddedToContainer += Component_ComponentRegisteredAndAddedToContainer;
		//	this.m_designSurface.DesignContext.Services.Component.ComponentRegistered += Component_ComponentRegistered;
		//	this.m_designSurface.DesignContext.Services.Component.ComponentRemoved += Component_ComponentRemoved;
		//}

		//private void Component_ComponentRemoved(object sender, DesignItemEventArgs e)
		//{
			
		//}

		//private void Component_ComponentRegistered(object sender, DesignItemEventArgs e)
		//{
			
		//}

		//private void Component_ComponentRegisteredAndAddedToContainer(object sender, DesignItemEventArgs e)
		//{
			
		//}

		public static MyDesignerModel Instance = new MyDesignerModel();

		private DesignSurface m_designSurface;


		public DesignSurface DesignSurface
		{
			get { return this.m_designSurface; }
		}

		public IPropertyGrid PropertyGrid { get; set; }


		private IOutlineNode m_outlineNode = null;
		public IOutlineNode OutlineRoot
		{
			get { return this.m_outlineNode; }
			set { this.m_outlineNode = value; NotifyPropertyChanged("OutlineRoot"); }
		}

		// ------------------------------------------------------------------------------------
		#region INotifyPropertyChanged Members
		/// <summary>
		/// Fire the PropertyChanged event for the given property
		/// </summary>
		/// <param name="propertyName"></param>
		protected void NotifyPropertyChanged(String propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		/// <summary>
		/// Property value has changed
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
