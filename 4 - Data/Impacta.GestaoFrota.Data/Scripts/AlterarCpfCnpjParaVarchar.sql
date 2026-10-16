BEGIN;

ALTER TABLE frota.funcionario
	ALTER COLUMN cpf TYPE character varying(11)
	USING cpf::text::character varying(11);

ALTER TABLE frota.pessoa
	ALTER COLUMN cpf TYPE character varying(11)
	USING cpf::text::character varying(11);

ALTER TABLE frota.pessoa
	ALTER COLUMN cnpj TYPE character varying(14)
	USING cnpj::text::character varying(14);

COMMIT;
