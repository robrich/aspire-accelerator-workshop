<template>
  <div v-if="frameworks.loading" class="text-center">
    Loading...
  </div>
  <div v-else-if="frameworks.error" class="text-center text-danger">
    Error: {{ frameworks.error.message }}
  </div>
  <div v-else>
    <div class="card">
      <div class="card-body">
        <form @submit.stop.prevent="submitNewFramework">
          <div class="row">
            <div class="col">
              <input type="text" class="form-control" id="name" v-model="newName" placeholder="Add a new Framework" required maxlength="200" />
            </div>
            <div class="col">
              <button type="submit" class="btn btn-primary">Add Framework</button>
            </div>
          </div>
        </form>
      </div>
    </div>
    <div class="row mt-4">
      <div class="col-md-8">
        <h2 class="text-center">Frameworks</h2>
      </div>
      <div class="col-md-4 text-end">
        <button class="btn btn-primary" @click="refresh">Refresh</button>
      </div>
    </div>
    <div class="row">
      <table class="table">
        <thead>
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Name</th>
            <th scope="col">Votes</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="framework in frameworkScores" :key="framework.id">
            <FrameworkRow :framework="framework" @save-framework="submitChangeFramework" @save-vote="submitVote" />
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup vapor lang="ts">
import { computed, ref } from 'vue';
import { useFrameworkGet, useFrameworkNew, useFrameworkUpdate } from '@/hooks/use-framework';
import { useVoteGet, useVoteScore } from '@/hooks/use-vote';
import type { Framework } from '@/types/framework';
import FrameworkRow from '@/components/FrameworkRow.vue';
import type { VoteScore } from '@/types/vote-score';
import type { FrameworkScore } from '@/types/framework-score';


const { results: frameworks, fetch: refreshFrameworks } = useFrameworkGet();
const { fetch: saveNewFramework } = useFrameworkNew();
const { fetch: saveChangeFramework } = useFrameworkUpdate();
const { results: votes, fetch: refreshVotes } = useVoteGet();
const { fetch: saveVote } = useVoteScore();

const newName = ref('');

const frameworkScores = computed((): FrameworkScore[] => {
  console.log('Recomputing frameworkScores', frameworks.value?.data);
  const list = frameworks.value?.data ?? [];
  return list.map(framework => {
    const vote = votes.value.data?.find(v => v.frameworkId === framework.id);
    return {
      ...framework,
      score: vote?.score ?? 0
    } as FrameworkScore;
  });
});

async function submitNewFramework() {
  const newFramework: Framework = {
    id: 0, // new
    name: newName.value
  };
  await saveNewFramework({ body: newFramework });
  newName.value = '';
  await refresh();
}

async function refresh() {
  await refreshFrameworks();
  await refreshVotes();
}

async function submitChangeFramework(updatedFramework: Framework) {
  await saveChangeFramework({ id: updatedFramework.id, body: updatedFramework });
  await refresh();
}

async function submitVote(vote: VoteScore) {
  await saveVote(vote);
  await refresh();
}
</script>

<style scoped>
</style>
