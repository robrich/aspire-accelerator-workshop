-- Postgres init script

-- Create the table
CREATE TABLE IF NOT EXISTS public.frameworks (
    id SERIAL PRIMARY KEY,
    name VARCHAR(200) NOT NULL
);

CREATE TABLE IF NOT EXISTS public.votes (
    id SERIAL PRIMARY KEY,
    frameworkid INTEGER REFERENCES public.frameworks(id),
    score INTEGER NOT NULL DEFAULT 0
);
