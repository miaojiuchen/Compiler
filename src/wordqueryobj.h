#pragma once

#include<m_stdafx.h>
#include<queryobj.h>

using std::string;

class WordQueryObj :public QueryObj
{
	friend class QueryExpression;
private:
	WordQueryObj(const string &);

	QueryResult eval(const TextQuerier &) const override;
	string get_exp() const override;

private:
	string m_target;

};