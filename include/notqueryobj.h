#pragma once

#include<queryobj.h>
#include<queryexpression.h>

class NotQueryObj:public QueryObj
{
	friend QueryExpression operator!(const QueryExpression &);
private:
	NotQueryObj(const QueryExpression &); 

	QueryResult eval(const TextQuerier &) const override;
	string get_exp() const override;

private:
	QueryExpression m_qexp;

};

inline NotQueryObj::NotQueryObj(const QueryExpression &q) :m_qexp(q)
{
}

inline QueryExpression operator!(const QueryExpression &exp)
{
	return QueryExpression(shared_ptr<QueryObj>(new NotQueryObj(exp)));
}