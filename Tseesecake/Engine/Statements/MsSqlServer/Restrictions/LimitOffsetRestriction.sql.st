LimitOffsetRestriction(restriction)::=$\\$
OFFSET $restriction.Offset$ ROWS
FETCH NEXT $restriction.Value$ ROWS ONLY
