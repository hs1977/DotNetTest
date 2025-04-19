using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

	class CTest
	{
		public CTest() 
		{ 
		}

		/*
		// < 소멸자 및 Finalize >

		1.	소멸자가 정의되면 암묵적으로 Finalize가 정의된다. 
			따라서 소멸자가 있는 상태로 Finalize를 명시적으로 정의할 경우 빌드 에러가 발생한다.
		2.	소멸자가 정의되지 않은 채로 Finalize를 명시적으로 정의할 수 있으나 실제로 GC의 Collect가 동작할 때 호출되지 않는다.
			즉, GC Collect에서 호출되는 것은 호출자라고 할 수 있다.
		*/ 
		~CTest()	// 소멸자에 한정자는 유효하지 않다.
		{
			System.Console.WriteLine("CTest::~CTest");
		}

		/*
		public void Finalize()
		{
			System.Console.WriteLine("CTest::Finalize");
		}
		*/

		public void MakeChild()
		{
			m_Child = new CChild();		// C++과 다르게 new 뒤에는 괄호()를 사용하여 생성자를 호출해야 한다. new T();
		}

		public void Show()
		{
			System.Console.WriteLine(ToString());
		}


		public CChild m_Child;			// C#에서는 기본으로 null로 설정된다.
	}
	

	internal class Program
	{
		static void Main(string[] args)
		{
			System.Console.WriteLine("<START>");

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

			CTest t = new CTest();
			t.Show();
			t.MakeChild();
			t.m_Child.Func();
			t.m_Child.VFunc();
			System.Console.WriteLine("-----------------------------------");

			/*
			// 강제로 GC 실행

			t = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
			*/

			System.Console.WriteLine("<END>");
		}
	}
}
