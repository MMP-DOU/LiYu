Shader "Myshader"{//����ָ��shader�����֣���Ҫ����ļ�������һ��
	
	Properties{
		//����,ϣ���ⲿ���ڵ����Կ��ԷŽ���
		//���ͣ�
		_Color("base color",Color)=(1,1,1,1)//������("��ʾ��������"��������)=(R,G,B,Alpha)Ĭ��ֵ
		_Vector("vector",Vector)=(1,2,3,4)
		_Int("int",Int)=114514
		_Float("float",Float)=1.198
		_Range("range",Range(1,10))=6
		_2D("Texture",2D) = "white"{}
		_Cube("cube",Cube) = "white"{}
		_3D("3dtexture",3D) = "black"{}
		}
	SubShader{
		//��Shader�������кܶ������д��Ⱦ���� SubShader������Կ�����Ч��ʱ���ӵ�һ��SubShader��ʼ�������һ�������Ч������ʵ�֣����õ�һ���������Զ�������һ����
		//һ��Pass���൱��һ��������������һ��Pass
		Pass{
			//�������дshader����
			CGPROGRAM
			//ʹ��CG���Ա�дshader����

			float4 _Color;//���������������Ҫ���¶���һ�£����ø�Ĭ��ֵ,����Ҫ�ӷֺ�
			float _Int;
			sampler2D _2D;
			samplerCube _Cube;
			sampler3D _3D;

			ENDCG
			}
	}
	Fallback "VertexLit" //�󱸷�����ָ��һ��shader�����涼���˾������
}