#pragma once

#include<binaryqueryobj.h>

class AndQueryObj :public BinaryQueryObj
{
	friend QueryExpression operator&(const QueryExpression &, const QueryExpression &);
private:
	AndQueryObj(const QueryExpression &,const QueryExpression &);

	QueryResult eval(const TextQuerier &) const override;

};

inline AndQueryObj::AndQueryObj(const QueryExpression &lexp, const QueryExpression &rexp)
	:BinaryQueryObj(lexp, rexp, "&")
{
}

inline QueryExpression operator&(const QueryExpression &lexp, const QueryExpression &rexp)
{
	return QueryExpression(shared_ptr<QueryObj>(new AndQueryObj(lexp, rexp)));
}