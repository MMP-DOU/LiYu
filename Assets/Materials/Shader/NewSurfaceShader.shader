Shader "Myshader"{//这里指定shader的名字，不要求和文件名保持一致
	
	Properties{
		//属性,希望外部调节的属性可以放进来
		//类型：
		_Color("base color",Color)=(1,1,1,1)//属性名("显示的属性名"，类型名)=(R,G,B,Alpha)默认值
		_Vector("vector",Vector)=(1,2,3,4)
		_Int("int",Int)=114514
		_Float("float",Float)=1.198
		_Range("range",Range(1,10))=6
		_2D("Texture",2D) = "white"{}
		_Cube("cube",Cube) = "white"{}
		_3D("3dtexture",3D) = "black"{}
		}
	SubShader{
		//子Shader，可以有很多个，编写渲染代码 SubShader多个，显卡运行效果时，从第一个SubShader开始，如果第一个里面的效果都能实现，就用第一个，否则自动运行下一个。
		//一个Pass块相当于一个方法，至少有一个Pass
		Pass{
			//在这里编写shader代码
			CGPROGRAM
			//使用CG语言编写shader代码

			float4 _Color;//上面的属性在这里要重新定义一下，不用赋默认值,这里要加分号
			float _Int;
			sampler2D _2D;
			samplerCube _Cube;
			sampler3D _3D;

			ENDCG
			}
	}
	Fallback "VertexLit" //后备方案，指定一个shader，上面都寄了就用这个
}