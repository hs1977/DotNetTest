using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
	class CParent
	{
		public CParent() { }
		public void Func()
		{
			System.Console.WriteLine("CParent::Func");
		}

		public virtual void VFunc()
		{
			System.Console.WriteLine("CParent::VFunc");
		}
	}

	class CChild : CParent 
	{
		public CChild() { }

		public new void Func()			// new가 없으면 Warning이 발생한다.
		{
			System.Console.WriteLine("CChild::Func");
		}

		public override void VFunc()	// override가 있어야 가상함수로 동작한다.
		{
			System.Console.WriteLine("CChild::VFunc");
		}
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			CChild c = new CChild();
			c.Func();
			c.VFunc();
			System.Console.WriteLine("-----------------------------------");

			CParent p = c;
			p.Func();
			p.VFunc();
			System.Console.WriteLine("-----------------------------------");

			((CParent)c).Func();
			((CParent)c).VFunc();
			System.Console.WriteLine("-----------------------------------");
		}
	}
}
