#include<m_stdafx.h>

#include<queryexpression.h>
#include<notqueryobj.h>
#include<andqueryobj.h>
#include<orqueryobj.h>
#include<queryhistory.h>

using std::cout;
using std::endl;

int main(int argc, string *argv)
{
	ifstream ifs("F:\\RegularExpression\\Debug\\file1.txt");
	TextQuerier tq(ifs);

	QueryExpression q1 = !QueryExpression("over");
	QueryExpression q2 = QueryExpression("over");
	QueryExpression q3 = QueryExpression("over") & QueryExpression("Stack");

	QueryHistory archives;
	archives.addRecord(q1);
	archives.addRecord(q2);
	archives.addRecord(q3);
	archives.addRecord(q1|q2);

	QueryResult res = archives[3].eval(tq);
	cout << res << endl;

	getchar();
}