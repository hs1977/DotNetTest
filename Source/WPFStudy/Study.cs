using System;
using System.Collections;
using System.Windows;

namespace WPFStudy
{
	/*
	// < ClickOnce 배포 >

	1.	속성>서명 : 인증서가 필요하다.

	2.	속성 > 게시
	2-1.	게시 위치 > 폴더 위치 게시 : Setup 파일들 만들어지는 위치(프로젝트 위치 상대 경로 지정)
	2-2.	게시 위치 > 설치 폴더 URL : 프로그램 업데이트시 확인하는 URL(최초 설치시에도 확인한다. 따라서 2-1에서 만들어진 Setup 파일들을 Web URL에 올려야 한다.)
	2-3.	설치 모드 및 설정 > 오프라인으로도 어플리케이션 사용 가능 Check
			어플리케이션 파일에 특정 파일을 포함시키기 위해서는 특정 파일 속성에서 '빌드 작업'을 설정해줘야 한다.

	3.	ClickOnce를 이용할 경우 Local PC에 설치되는 위치는 다음과 같이 지정된다. C:\Users\HS1977\AppData\Local\Apps\2.0\*
		즉, 설치 경로를 원하는 곳에 만들 수 없다.
	*/

	class Study
	{
		enum ETest
		{
			EA = 10,
			EB = 11,
			EC = 12
		}

		public static void EnumTest()
		{
			/*
			// < Enum Formatting >

			1.	Enum 값을 {#}으로 Formatting 할 경우 숫자가 아닌 문자열 값이 나온다.
				만일 숫자를 나오게 하려면 {#:D}를 사용한다.
			*/

			ETest e = ETest.EA;

			Array arrEnum = Enum.GetValues(e.GetType());
			for (int i = 0; i < arrEnum.Length; i++)
			{
				Console.WriteLine("{0}, {0:D}", arrEnum.GetValue(i));
				
			}
		}

		struct STest
		{
		//	public STest() { }	// Error - 구조체는 매개 변수가 없는 명시적 생성자를 포함할 수 없습니다.

			public STest(int arg1, int arg2)
			{
				m_Value1 = arg1;
				m_Value2 = arg2;
			}

			public int m_Value1;
			public int m_Value2;
		}

		public static void StructTest()
		{
			/*
			// < ValueType Consturctor with new >

			1.	new Type 뒤에는 반드시 (), [], {}가 필요하다.
				new Type(...)
				new Type[...]
				new Type{...}
				
			2.	구조체의 암시적 기본 생성자는 모든 멤버 데이터를 0 or null로 초기화한다.

			3.	구조체는 매개 변수가 없는 명시적 생성자를 포함할 수 없다.

			4.	구조체의 생성자를 호출하기 위해서는 new를 사용해야 한다.

			5.	구조체의 생성자는 모든 멤버 데이터를 초기화해야 한다.
			*/
			
			int i = 1;
		//	int i = new int;		// Error
		//	int i = new int(1);		// Error

		//	STest s0(1, 2);			// Error - 함수로 취급된다.

			STest s1;				// new를 사용하지 않을 경우 기본 생성자가 호출된다.
			s1.m_Value1 = 1;
			s1.m_Value2 = 2;

		//	STest s2 = new STest;	// Error
			STest s2 = new STest();	// new는 Stack에 생성된 s2의 멤버 데이터를 생성자를 통해서 초기화하는 역할을 한다.
			s2.m_Value1 = 1;
			s2.m_Value2 = 2;
			
			STest s3 = s2;			// s2, s3는 Stack에 생성된 각각 다른 객체를 나타낸다.
			s3.m_Value1 = 3;

			s2 = new STest(4, 5);	// new는 Stack에 생성된 s2의 멤버 데이터를 생성자를 통해서 초기화하는 역할을 한다.

			Console.WriteLine("{0}, {1}", s1.m_Value1, s1.m_Value2);
			Console.WriteLine("{0}, {1}", s2.m_Value1, s2.m_Value2);
			Console.WriteLine("{0}, {1}", s3.m_Value1, s3.m_Value2);
		}

		class CParent
		{
			public void Func()
			{
				System.Console.WriteLine("CParent::Func");
			}

			public virtual void VFunc()
			{
				System.Console.WriteLine("CParent::VFunc");
			}
		}

		class CTest : CParent
		{
			public IEnumerator GetEnumerator()		// IEnumerable을 상속하지 않아도 된다.
			{
				yield return 11;
				yield return 12;
				yield return 13;
				yield return 14;
			}

			public IEnumerable DoForeach()			// 반복자에 이름 붙이기
			{
				yield return 21;
				yield return 22;
				yield return 23;
				yield return 24;
			}

			public CTest()
			{
			}

			public CTest(int arg)
			{
				m_Value1 = arg;
			}

			public new void Func()		// 여기서 new가 없으면 Warning 발생
			{
				System.Console.WriteLine("CTest::Func");
			}

			public override void VFunc()
			{
				System.Console.WriteLine("CTest::VFunc");
			}

			public int m_Value1;
			public int m_Value2;
		}

		public static void ClassTest()
		{
			/*
			// < class Consturctor with new >

			1.	new Type 뒤에는 반드시 (), [], {}가 필요하다.
				new Type(...)
				new Type[...]
				new Type{...}
				
			2.	클래스의 암시적 기본 생성자는 모든 멤버 데이터를 0 or null로 초기화한다.
			*/

			{
				CTest t = new CTest(1);
				Console.WriteLine("CTest m_Value: {0}, {1}", t.m_Value1, t.m_Value2);

				foreach (int i in t)
				{
					Console.WriteLine("{0}", i);
				}

				foreach (int i in t.DoForeach())
				{
					Console.WriteLine("{0}", i);
				}
			}

			{
				CParent p = new CTest();
				p.Func();
				p.VFunc();
			}
		}

		[STAThread]
		public static void Main()
		{
			EnumTest();
			StructTest();
			ClassTest();

			StudyApplication app = new StudyApplication();
			app.ShutdownMode = ShutdownMode.OnMainWindowClose;
			app.Run();
		}
	}
}
