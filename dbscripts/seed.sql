
\connect keydb

CREATE TABLE apikey
(
    id serial PRIMARY KEY,
    key  UUID NOT NULL,
    vendor  VARCHAR (100)  NOT NULL,
    request_count int NOT NULL DEFAULT(0),
    created_at TIMESTAMP DEFAULT NOW()
);

ALTER TABLE "apikey" OWNER TO keyuser;

Insert into apikey(key,vendor) values( '32143b12-6f6d-490a-9a8a-fdbb73328682','Vendor 1');
Insert into apikey(key,vendor) values( 'dd36bc29-4fff-496c-841c-f3a98bb099e8','Vendor 2');
