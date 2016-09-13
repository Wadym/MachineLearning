// MathNumRec.cpp : Defines the exported functions for the DLL application.
#include "nr3.h"
#include "ludcmp.h"
#include <iostream>
#include <fstream>
#include <string>
using namespace std;
//extern "C" { __declspec(dllexport) bool SolveMatrixEq(double * a, double * b, double * x, int m); }
extern "C" { __declspec(dllexport) bool SolveMatrixEq(); }
//extern "C" { __declspec(dllexport) double* __stdcall  SolveMatrixEqDir(double* a, double* b, int m);}}
extern "C" { __declspec(dllexport) bool __stdcall SolveMatrixEqDir(double* a, double* b, int m, double*dll);
//extern "C" { __declspec(dllexport) double fun1(); }
//double fun1(){return (double)1177.0;}
//bool SolveMatrixEq(double * a, double * b, double * x, int m){
bool SolveMatrixEq(){
	double * a;
	double * b;
	double * x;
	int m;
	ofstream ofile;
	ifstream ifile;
	ifile.open ("d:\\work\\projects\\CPE_Sync\\CPE\\Data\\Internal\\dims.tsv");
	string strEl;
	ifile>>strEl;
	ifile.close();
	m=atoi(strEl.c_str());
	a=new double[m*m];
	ifile.open ("d:\\work\\projects\\CPE_Sync\\CPE\\Data\\Internal\\matrixAA.tsv");
	int indx=0;
	while((!ifile.eof()) && (indx<(m*m)))
	{
		ifile>>strEl;
		a[indx++]=atof(strEl.c_str());
	}
	ifile.close();
	indx=0;
	ifile.open ("d:\\work\\projects\\CPE_Sync\\CPE\\Data\\Internal\\matrixBB.tsv");
	b=new double[m*m];
	while((!ifile.eof()) && (indx<(m*m)))
	{
		ifile>>strEl;
		b[indx++]=atof(strEl.c_str());
	}
	ifile.close();
	x=new double[m];



	static const Int n = m;
	MatDoub aa(n,n);
	VecDoub bb(n),xx(n);

	for (Int i = 0; i < m; i++)
	{
		for (Int j = 0; j < m; j++)
		{
			aa[i][j] = a[i*(m-1)+j];
		}
	}
	int indc=0;
	for (Int i = 0; i < (m*m); i++)
	{
		if(fabs(fmod((double)i,(double)(m)))<=0.01)
		{
			bb[indc++] = b[i];
		}
	}

	LUdcmp alu(aa);
	alu.solve(bb,xx);
	Int N = (Int)(xx.size());
	for(Int i=0;i<N;i++){
		x[i]=xx[i];
	}
	//a=aa;
	ofile.open ("d:\\work\\projects\\CPE_Sync\\CPE\\Data\\Internal\\matrixX.tsv");
	for(Int i=0;i<N;i++)
	{
		ofile<<x[i]<<"\t";
	}
	ofile.flush();
	ofile.close();
	delete [] a;
	delete [] b;
	return true;
}
bool __stdcall SolveMatrixEqDir(double* a, double* b, int m, double*xdll)
{
	double * adll;
	double * bdll;
	//double * xdll;
	adll=new double[m*m];
	int indx=0;
	while(indx<(m*m))
	{
		adll[indx]=a[indx];
		indx++;
	}
	indx=0;
	bdll=new double[m*m];
	while(indx<(m*m))
	{
		bdll[indx]=b[indx];
		indx++;
	}
	//xdll=new double[m];

	static const Int n = m;
	MatDoub aa(n,n);
	VecDoub bb(n),xx(n);

	for (Int i = 0; i < m; i++)
	{
		for (Int j = 0; j < m; j++)
		{
			aa[i][j] = adll[i*(m-1)+j];
		}
	}
	int indc=0;
	for (Int i = 0; i < (m*m); i++)
	{
		if(fabs(fmod((double)i,(double)(m)))<=0.01)
		{
			bb[indc++] = bdll[i];
		}
	}

	LUdcmp alu(aa);
	alu.solve(bb,xx);
	Int N = (Int)(xx.size());
	for(Int i=0;i<N;i++){
		xdll[i]=xx[i];
	}
	delete [] adll;
	delete [] bdll;
	//return xdll;
	return true;
}
}
