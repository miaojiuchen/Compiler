#pragma once

#include<m_stdafx.h>
#include<queryobj.h>
#include<wordqueryobj.h>

using std::string;
using std::shared_ptr;
using std::ostream;

class QueryExpression
{
	friend QueryExpression operator!(const QueryExpression &);
	friend QueryExpression operator&(const QueryExpression &, const QueryExpression &);
	friend QueryExpression operator|(const QueryExpression &, const QueryExpression &);
public:
	QueryExpression(const string &);//for word query expression

	QueryResult eval(const TextQuerier &) const;
	string get_exp() const;

private:
	QueryExpression(shared_ptr<QueryObj>);//for composite expression

private:
	shared_ptr<QueryObj> m_pQueryObj;

};

inline QueryExpression::QueryExpression(const string &target)
	:m_pQueryObj(new WordQueryObj(target))
{
}

inline QueryExpression::QueryExpression(shared_ptr<QueryObj> p)
	: m_pQueryObj(p)
{
}

inline ostream &operator<<(ostream &os, const QueryExpression &qe)
{
	return os << qe.get_exp();
}