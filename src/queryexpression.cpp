#include<queryexpression.h>
#include<notqueryobj.h>
#include<andqueryobj.h>
#include<orqueryobj.h>

QueryResult QueryExpression::eval(const TextQuerier &tq) const
{
	return m_pQueryObj->eval(tq);
}

string QueryExpression::get_exp() const
{
	return m_pQueryObj->get_exp();
}
