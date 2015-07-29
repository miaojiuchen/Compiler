#pragma once

#include<m_stdafx.h>
#include<textquerier.h>

class QueryObj
{
	friend class QueryExpression;
protected:
	virtual ~QueryObj() = default;

private:
	virtual QueryResult eval(const TextQuerier &) const = 0;
	virtual string get_exp() const = 0;

};