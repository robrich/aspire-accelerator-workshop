<template>
  <td scope="row">
    <span class="pt-2 px-3 d-inline-block">
      {{ framework.id }}
    </span>
  </td>
  <td class="flex-grow-1" v-if="!isEditing" @click="changeEdit">
    <button class="btn btn-secondary mt-2 ms-2">
    <i class="fa-solid fa-pencil"></i>
    </button>
    <span class="pt-2 px-3 d-inline-block">
      {{ framework.name }}
    </span>
  </td>
  <td v-else>
    <div class="row">
      <div class="col-auto">
        <button class="btn btn-secondary mt-2 ms-2" @click="changeEdit">
          <i class="fa-solid fa-xmark"></i>
        </button>
      </div>
      <div class="col-auto">
        <form @submit.prevent="saveEdit">
          <div class="d-flex">
            <input v-model="editName" class="form-control me-2" required maxlength="200" />
            <button type="submit" class="btn btn-primary mt-2">
              <i class="fa-solid fa-floppy-disk"></i>
            </button>
          </div>
        </form>
      </div>
    </div>
  </td>
  <td>
    <button class="btn btn-secondary mt-2 ms-2" @click="voteDown"><i class="fa-solid fa-minus"></i></button>
    <span class="pt-2 px-3 d-inline-block">
      {{ framework.score }}
    </span>
    <button class="btn btn-secondary mt-2 ms-2" @click="voteUp"><i class="fa-solid fa-plus"></i></button>
  </td>
</template>

<script setup vapor lang="ts">
import { ref } from 'vue';
import type { Framework } from '@/types/framework';
import type { FrameworkScore } from '@/types/framework-score';
import type { VoteScore } from '@/types/vote-score';

// <FrameworkRow :framework="framework" @saveFramework="onSaveFramework" @saveVote="saveVote" />
const emitSaveFramework = defineEmits<{
  (e: 'save-framework', payload: Framework): void
  (e: 'save-vote', payload: VoteScore): void
}>();
const { framework } = defineProps<{
  framework: FrameworkScore
}>();

const isEditing = ref(false);
const editName = ref(framework.name);

function changeEdit() {
  isEditing.value = !isEditing.value;
  if (!isEditing.value) {
    editName.value = framework.name;
  }
}
async function saveEdit() {
  const updatedFramework: Framework = {
    id: framework.id,
    name: editName.value
  };
  isEditing.value = false;
  emitSaveFramework('save-framework', updatedFramework);
}

function voteUp() {
  const newVote: VoteScore = {
    frameworkId: framework.id,
    score: 1
  };
  emitSaveFramework('save-vote', newVote);
}
function voteDown() {
  const newVote: VoteScore = {
    frameworkId: framework.id,
    score: -1
  };
  emitSaveFramework('save-vote', newVote);
}
</script>

<style scoped>
</style>
