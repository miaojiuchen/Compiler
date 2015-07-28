#pragma once

#include<queryobj.h>
#include<queryexpression.h>

class BinaryQueryObj :public QueryObj
{
protected:
	BinaryQueryObj(const QueryExpression &, const QueryExpression &, string);

	string get_exp() const override;

	QueryExpression m_lexp, m_rexp;
	string exp_symbol;

};

inline BinaryQueryObj::BinaryQueryObj(const QueryExpression &lexp, const QueryExpression &rexp, string s)
	:m_lexp(lexp), m_rexp(rexp), exp_symbol(s)
{
}