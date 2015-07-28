#pragma once
#include<binaryqueryobj.h>

class OrQueryObj :public BinaryQueryObj
{
	friend QueryExpression operator|(const QueryExpression &, const QueryExpression &);
private:
	OrQueryObj(const QueryExpression &, const QueryExpression &);

	QueryResult eval(const TextQuerier &) const override;

private:
	QueryExpression m_lexp, m_rexp;
};

inline OrQueryObj::OrQueryObj(const QueryExpression &lexp, const QueryExpression &rexp)
	:BinaryQueryObj(lexp, rexp, "|"), m_lexp(lexp), m_rexp(rexp)
{
}

inline QueryExpression operator|(const QueryExpression &lexp, const QueryExpression &rexp)
{
	return QueryExpression(shared_ptr<QueryObj>(new OrQueryObj(lexp, rexp)));
}
